# Testable WebForms with MVP

'Testable ASP.NET WebForms with MVP' was developed based off of a series of user group presentations which covered MVP, Refactoring Legacy Code, and Unit Testing. The code found here is built to be a step by step guide showing readers how to move from an already built ASP.NET WebForms project to one making use of MVP to allow unit testing. 

This presentation was original given at SQL Saturday Baton Rouge 2014.

**Explanation of Source**
The code shows a progression from "classic" web forms (v1) to fully testable MVP based web forms (v5). When you open the solution you'll find multiple Solution Folders. Each folder has a copy of the sample application moving one step closer to implemented MVP. Folders are as follows:

 - Version 1 - The "original" application. This has SQL in the code behind as well as in the declarative markup. 
 - Version 2 - This version introduces objects and refactored out the data layer using the Repository pattern. SqlDataSources are still found on the list pages. 
 - Version 3 - This version introduces presenters and view contracts. SqlDataSources have been replaced by ObjectDataSources on list pages. No view contracts for list pages. 
 - Version 4 - This version introduces a unit testing project and unit tests. 
 - Version 5 - This version introduces a view contract and filtering to the person list page. 
