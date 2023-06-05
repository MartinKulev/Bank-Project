# Bank-Project
A console app that represents a bank. You need to change the connection string, according to your database in order for the app to work.

# How to use the program?
You choose an option by typing in the number corresponding to that option and then pressing enter. After typing in any information you press enter to continue.

# Exit
If you choose this option the code stops executing.

# Login and Register
You will first be faced with 2 options: Login and Register. When you choose "Register" it will show you a view where you can type in your credentials.
They are the following: EGN, First name, Last name, Email adress and your choice of a 4-digit PIN code. You need to remeber your PIN and your IBAN.
Once you fill the fields with a valid information you will be shown a view where a random card number is generated for you. You need to remember your card number.
Then you can choose "Login". This will show you a view where you can type in your card number and PIN, that you used to register with.
Upon entering valid information you will be shown the Bank Menu view.

# Bank Menu
The Bank Menu has 8 options. They are: Show Balance, Withdraw Money. Deposit Money, Transfer Money, Show Credit Info, Take Credit, Pay Credit, Logout.

# Show Balance
Upon choosing this option you will be shown a view in which you can see your balance.

# Withdraw Money
Upon choosing this option you will be shown a view where you can type in the amount you wish to withdraw.
Then by pressing enter you can withdraw the amount you wrote if the number entered is valid.

# Deposit Money
Upon choosing this option you will be shown a view, in which you can type in the amount you wish to deposit. 
Then by pressing enter you can deposit the amount you wrote if the number entered is valid.

# Transfer Money
Upon choosing this option you will be shown a view, in which you can type in the IBAN of the person you are sending money to and the amount of money you wish to trasnfer.
Then by pressing enter you can transfer the amount you wrote to the IBAN you wrote.
Upon entering valid information the money will be sent.

# Show Credit Info
Upon pressing this button you will be shown a view, in which you can see if you have an existing credit and if you have one - you can see the information about the credit.

# Take Credit
Upon pressing this button you will be shown a view with 3 options for a credit. You can only take 1 credit at a time.
If you have an existing credit, you will not be able to take another credit until you pay your current one and an error message will be displayed. 
If you don't have an existing credit and you press one of the buttons for credit you will be taken to another page with a message that you succesfully took a credit and an updated balance will be shown.
You can press "Go Back" at any time to return to the Bank Menu page.

# Pay Credit
Upon pressing this button you will be taken to a page with a message, according to your credit payability status. You can press "Go Back" at any time to return to the Bank Menu page.

# Logout
Upon pressing this button you will be taken to the starter page. There you can Login or Register.
