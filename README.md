Solution to penalty shooters problem defined here;

https://practice.geeksforgeeks.org/problems/penalty-shooters/0#ExpectOP

Approach taken;

First, note that entities involved are all footballers, though we have two roles of footballers; Strikers and Goalies. Strikers score goals and Goalies prevent them. Both have a common energy property, but how that property updates depends on their type.
Additionally, we want to bind the goals scored to which Striker scored it for the return, so included that as a property of a Striker. We are only considering the case of a single game, if we were considering case of multiple games in a season this would need to be a little different.

So, I defined class structure to match this. All Footballers have an energy property, Strikers inherit this and define a method to decrement their energy and update their score total when they score. Goalies decrement their energy when they save.

After that, it was a case of iterating over Strikers to see when they could score and update their properties as appropriate. With some consideration, noted that when a Striker scores, the game state only changes for that Striker, not for any other footballer. This lead to the logic of "if a striker can score, they should do so until they can no longer score. The order of strikers scoring does not matter".
In a more complex case, we would need to consider the optimal order in which Strikers should take their shots to maximise the total score. For example, if the goalie lost energy on every scored goal, then there would be cases where it would be better for the team for a striker who could score to let another striker shoot first. In this example though, that was not the case.

The solution does not fundamentally change when adding more than two strikers, as the striker's actions are independent, so included logic to support as many strikers as desired, though only one goalie. Should not matter to result.

Tested solution manually against examples provided in test and also wrote unit tests to verify the functionality of the calculation (separated out from logic to read input from command line).

For fun, also added a method to calculate the result using a regressive approach and using LINQ. This isn't really necessary for the solution and would not be included in production, just an alternative solution that was neat. Is a little slower than the iterative solution, though.
