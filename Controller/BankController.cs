using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Bank_Application.Data;
using Bank_Application.Service;
using Bank_Application.View;

namespace Bank_Application.Controller
{
    public class BankController
    {
        private BankService bankService;
        private AppView view;
        private bool isRunning;
        private UserBankInfo userBankInfo;
        private UserInfo userInfo;
        public BankController(BankService bankService, AppView appView)
        {
            this.bankService = bankService;
            this.view = appView;
            this.isRunning = true;
        }

        public void Run(bool loginSuccess)
        {
           
            while (isRunning)
            {
                if(loginSuccess)
                {
                    view.PrintAllCommands();
                    ProcessAllCommands(view.CommandNumber);
                }
                else
                {
                    view.PrintAllStartupCommands();
                    ProcessAllStartupCommand(view.CommandNumber);
                }
               
            }
        }

        private void ProcessAllStartupCommand(int command)
        {
            switch (command)
            {
                case 1:
                    this.Register();
                    break;
                case 2:
                    this.LogIn();
                    break;
                case 3:
                    isRunning = false;
                    break;
            }
        }
        private void ProcessAllCommands(int command)
        {
            switch (command)
            {
                case 1:
                    this.ShowBalance();
                    break;
                case 2:
                    this.WithdrawMoney();
                    break;
                case 3:
                    this.DepositMoney();
                    break;
                case 4:
                    this.TransferMoney();
                    break;
                case 8:
                    this.LogOut();
                    break;
                case 0:
                isRunning = false;
                break;
            }
        }



        private void Register()
        {

            UserInfo userInfo = view.ReadRegisterUserInfo();
            string pin = view.ReadRegisterPin();
            string card_number = this.bankService.CreateRandomCardNumber();
            string iban = this.bankService.CreateRandomIBAN();
            UserBankInfo userBankInfo = new UserBankInfo(card_number, pin, userInfo.EGN, iban);
            CreditMoneyInfo creditMoneyInfo = new CreditMoneyInfo(card_number);
            CreditDateInfo creditDateInfo = new CreditDateInfo(card_number);
            CreditBooleanInfo creditBooleanInfo = new CreditBooleanInfo(card_number);

            bool registrationSuccess = true;
            if (userInfo.EGN.Length != 10)
            {
                view.WrongEgnCountMessage();
                registrationSuccess = false;
            }
            if (!userInfo.Email.Contains("@"))
            {
                view.EmailNotContainingATMessage();
                registrationSuccess = false;
            }
            if (userInfo.First_name == string.Empty || userInfo.Last_name == string.Empty)
            {
                view.EmptyNamesMessage();
                registrationSuccess = false;
            }
            if (pin.Length != 4)
            {
                view.IncorrectPINCountMessage();
                registrationSuccess = false;
            }
            using (BankContext context = new BankContext())
            {
                if (context.UserInfos.Contains(userInfo))
                {
                    view.UserAlreadyRegisteredMessage();
                    registrationSuccess = false;
                }
            }
            if (registrationSuccess)
            {
                view.SuccessfulRegistrationMessage(card_number, iban);
                this.bankService.RegisterUser(userInfo, userBankInfo);
            }
                bool loginSuccess = false;
                Run(loginSuccess);

        }
        private void LogIn()
        {
            string card_number = view.ReadLogInCardNumber();
            string pin = view.ReadLogInPIN();         
            userBankInfo = this.bankService.LogInUserInto1stTable(card_number);
            userInfo = this.bankService.LogInUserInto2ndTable(userBankInfo.EGN);
            bool loginSuccess = false;
            if (pin != userBankInfo.PIN)
            {
                view.WrongPINCodeMessage();
                loginSuccess = false;
            }
            else
            {
                view.SuccessfulLogInMessage(userInfo.First_name, userInfo.Last_name);
                loginSuccess = true;
            }
            Run(loginSuccess);
            
        }
        private void ShowBalance()
        {
            this.bankService.Balance(userBankInfo);
            view.ShowBalanceOutput(userBankInfo.Balance);
        }
        private void WithdrawMoney()
        {
            double withdrawAmount = view.WithdrawMoneyInput();            
            if(userBankInfo.Balance >= withdrawAmount)
            {
                userBankInfo.Balance -= withdrawAmount;
                this.bankService.WithdrawDeposit(userBankInfo);
                view.WithdrawMoneyOutput(withdrawAmount, userBankInfo.Balance);
            }
            else
            {
                view.WithdrawMoneyErrorMessage();
            }           
        }
        private void DepositMoney()
        {
            double depositAmount = view.DepositMoneyInput();
            userBankInfo.Balance += depositAmount;
            this.bankService.WithdrawDeposit(userBankInfo);
            view.DepositMoneyOutput(depositAmount, userBankInfo.Balance);
        }
        private void TransferMoney()
        {
            string ibanReceiving = view.TransferMoneyInputIBAN();
            double transferAmount = view.TransferMoneyInputAmount();
            
            if(transferAmount > userBankInfo.Balance)
            {
                view.TransferMoneyErrorMessage();
            }
            else
            {
                userBankInfo.Balance = this.bankService.Transfer(userBankInfo, ibanReceiving, transferAmount);
                view.TransferMoneyOutput(transferAmount, ibanReceiving, userBankInfo.Balance);
            }
        }
        public void LogOut()
        {
            bool loginSuccess = false;
            Run(loginSuccess);
        }

        
    }
}
