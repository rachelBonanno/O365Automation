O365Automation

Data Removed
Pages/Index.cshtml.cs - tenant ID and client ID <br>
docker-compose.yml - app name, Azure app configuration uri, Azure key vault shared uri, and Azure key vault app uri

Build
Run dotnet build

Test
Run dotnet test to run unit tests. Run docker compose --profile test up --abort-on-container-exit to run integration tests. Docker setup instructions can be found here. After running, you must cleanup your environment with docker compose --profile test down.

If you want to only run dependencies and not spin up your container or run the Postman integration tests with newman, you can run docker compose --profile debug up. After running, you must cleanup your environment with docker compose --profile debug down.

Local configuration
If you want to override local configuration, you can do so by creating a config.json file in the O365Automation folder.


What V1.0 (08/02/2023) Does:
- is a razor page that logs in to Microsoft and sends get requests to Microsoft Graph API
- the received data is entered into a table and the severity of each response is determined in the Index.cshtml.cs file
- the severity column is editable and will change color based on the severity input

To Do (Next Steps):
- this is not necessary but might make the code cleaner - if the CSS and JS info on the Index.cshtml file were put in there own file and then called from the Index file
- add a way for the user to enter the tenantId and the clientId on the webapp so that they have the ability to log in to any microsoft account they want
- for the first two columns of the table (category and control) automate the entry of that data by pulling from the excel sheet with all of the O365 questions so that the CS team only has to update that document to keep the web app up to date with the questions
- for the above point this may require coming up with a script to convert the get request to a format like in the Index.cshtml.cs file
- add the rest of the O365 questions (a lot of the questions have the same initial request and can be differentiated by the ControlName that is located under the ControlScores, this might be a good thing to add to the spreadsheet with the O365 questions so that after the get request it can be focused down by the control name, also each control name may have multiple instances but it looks like only one has a description so if you filter but the description not being null it fixes this issue but one thing to look into is if the most resent call is the only one with a description I did not have the time to see if that was true of if there was only an old instance with a description that was getting pulled in)
- allow the CS team to submit the data on the page and have it be saved to the client sheet 
- allow for the data that was updated on the sheet by the CS team to be saved between page reloads and logging in and out of the page
