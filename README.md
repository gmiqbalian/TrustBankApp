# TrustBankApp

This Solution has 3 projects: <br>
1- Trust Bank WebApp <br>
2- Trust Bank API <br>
3- Trust Bank Console App <br>

AZURE:https://trustbank.azurewebsites.net/

# 1- Trust Bank WebApp:
(using Razor Pages) <br>
Home page carry statistics for the number of customers, the number of accounts and the total balance of accounts (this page is public and does not require authentication).
All other pages require authentication and only the authorised user can handle the information they are allowed to.
The relevant user can see the bank customers details and the account details and perform relevant operations over customer profiles and their relevant accounts.
App uses both client side and server side validation for user input.

The users can carry 2 types of roles: <br>
1- Admins <br>
The role Admin is able to administer users. Only Admin user can perform CRUD functionality over the users and Cahisers do not have access to these pages.<br>
2- Cashiers <br>
Cashiers can administer customers and their accounts and have no access to Users.

By default the app generates 2 users with following credentials:<br>
User Name: richard.chalk@systementor.se" <br>
Password: "Hejsan123#" <br>
Role: "Admin"<br>
<br>
User Name: "richard.erdos.chalk@gmail.se"<br>
Password: "Hejsan123#"<br>
Role: "Cashier" <br>


# 2- Trust Bank API:
This API communicates with the Bank Database to retrieve information required. Cashiers are only authorised to read the data about customers and accounts.
User can perform two types of GET requests: <br>
1- GET/api/customer/id <br>
2- GET/api/account/id/{limit}/{offset} <br>
API uses JWT authorization to identify the allowed user.

# 3- Trust Bank ConsoleApp:
This console app generates reports about suspicious transactions over a specific time to help identify possible money laundering attempts. The reports is planned to schedule every night which will generate two type of reports. One report will print all single transactions over 15000 kr from the last reported date to this reporting date. The other report is focued on transactions the sume of which during the last three days (72h) from the current time and are greater than SEK 23000.
