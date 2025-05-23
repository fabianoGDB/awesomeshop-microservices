# awesomeshop-microservices

## TDD

Arrange
Where the items for the Unit Test are prepared.
This includes the definition and configuration of component dependencies, as well as the definition of test objects.
(Mocks are normally used)

Act
The action to be tested is executed.

Assert
Verifications of the final state are performed.

Given_When_Then Pattern
A pattern for naming Unit Tests, helping to use clearer and more objective names.

Example: if we are testing a handler called CreateProjectCommandHandler, we would use a name like:
InputDataIsOk_Executed_ReturnProjectId

Reading this in Portuguese as a sentence would result in something like:
“Given that the input data is correct, when executed, it should return the Project ID.”
