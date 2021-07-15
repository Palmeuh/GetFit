# GetFit

## Introduction

Project to help with motivation at the gym but mostly as coding practice. This is the third time redoing the project, as the first one was not planned well enough
and the lack of structure in the second one eventually made it too hard to understand. This led me to learning about project architecture and more about writing clean code.
I still have a long way to go but it's my best effort yet. 

## Things I've picked up so far

######2021-07-15
- DDS, though I'm not sure I'm doing it a 100% correct, but it helps me with having a concept in mind instead of mindlessly trying on my own.
- I had to learn more about ViewData and persisting state in MVC-applications.
- Sessions, as in my previous version of the project built the entire search/sort method through saving data in the session before I learned ViewData properly.
- The repository pattern. Though I've had an idea about it before, I've now learned to implement it through a generic repository. Next step is a Unit of Work. 
- Diving a little deeper into EF Core as a few bugs have emerged so far and probably a few to come...


## Current focuses

######2021-07-15

- The next step which is to add some user functionality. Creating workouts and programs, saving favorite ones, in general all of the good stuff.
- The search/sort in the controller indexes needs simplifying, first step is to refactor and break out some methods. I also need to learn more about returning
IQueryable vs IEnumerable in this method, I think I could make it more efficient though it's not top priority right now. This ties into that I have to call
ToUpper on both the searchstring and the db entity property to get the correct search results.

## Future plans

######2021-07-15
- Front end
- Testing
- Efficiency
- Security
