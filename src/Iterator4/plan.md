# `Iter<A>` version 4

## Take the lessons learned from `Iter3`:

* The co-routine stack-machine approach is a very good approach for implementing the more complex processes, like `bind`, `product`, `apply`, etc.
* The approach of building a large struct that holds variables, globals, and operations mostly worked well and was just about fast enough.
  * The problem was operators that generate new iterators (like `bind`) causes uncontrolled struct copying can cause non-linear performance issues depending on the nesting of the operators.
* I started to box (using an object-pool) the `Fields` structure within `Iter<A>` (`v3`), but that messes with the copying semantics of a half-mutable half-immutable type.

## Plan

* Create something simlar to `Iter<A>` version 3.
* Build bespoke memory management for the `Fields` structure.
  * Break apart the structure into its components, each with their own memory management strategy.
  * Split the type into mutable and immutable parts.
    * The mutable parts:
      * The variables that are pushed and popped during the running of an iterator.
      * The co-routine arguments
    * The immutable parts:
      * The globals and operations
  * As much as possible, use pooled memory, especially for the immutable parts. This will allow for the iterator structure itself to have a small footprint for immutable copying.
    
### Operations

Build an immutable list of operations. This can then be shared between multiple
iterators without copying.

### Globals

The initial definition of the globals should be immutable: so that they can be 
shared between multiple iterators without copying. Consider different approaches
to the in-place mutation of the globals that exists in `v3`.  Perhaps a better 
mechanism could be used, like a general variables system are access by ID (like 
`v3` globals) but are very much instances created from constants, rather than the
slightly mixed approach that exists in `v3`.

### Variables and stack-state

Use stack-based ref-structs that are setup at the start of the iterator and maintained
over the yields, so that functions can be resumed.  They will need to be cloned on 
every iteration, so keep them small.