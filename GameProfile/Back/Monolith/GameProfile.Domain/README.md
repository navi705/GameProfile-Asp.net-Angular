# Domain Layer
Domain Layer -


## Entities 
Entities - are entities that have identity, state and behaviour. They can be traceable, have links to other entities and have methods for changing their state. Entities usually represent real objects in the subject area, which may be unique, e.g. orders, users, etc.

## Value Object
In short, object value is a description of the object's motives. For example, if user has an email field,
to make sure it's correct, we have a value obejct in which we write the object's correctness rule.
https://en.wikipedia.org/wiki/Value_object

## Aggregate Root 
Aggregate Root - is an entity that is the root of an aggregate. An aggregate is a group of related entities that are to be modified as a whole. Aggregate Root is an entity which is the entry point for working with the aggregate. It ensures the integrity of the aggregate, controls access to other objects within the aggregate and determines which operations can be performed on the aggregate.