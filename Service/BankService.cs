using Bank_Application.Data;
using Bank_Application.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Application.Service
{
    public class BankService
    {
        private AppView view;
        public BankService(AppView appView)
        { 
            this.view = appView;
        }
        public void RegisterUser(UserInfo userInfo, UserBankInfo userBankInfo)
        {
            using (BankContext context = new BankContext())
            {
                context.UserInfos.Add(userInfo);
                context.UserBankInfos.Add(userBankInfo);                             
                context.SaveChanges();
            }
        }
        public string CreateRandomCardNumber()
        {
            Random random = new Random();
            string card_number1 = random.Next(1000, 9999).ToString();
            string card_number2 = random.Next(1000, 9999).ToString();
            string card_number3 = random.Next(1000, 9999).ToString();
            string card_number4 = random.Next(1000, 9999).ToString();

            string card_number = card_number1 + "-" + card_number2 + "-" + card_number3 + "-" + card_number4;
            return card_number;
        }
        public string CreateRandomIBAN()
        {
            string ibanCountry = "BG";
            Random random = new Random();
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string word = "";

            for (int i = 0; i < 16; i++)
            {
                int index = random.Next(alphabet.Length);
                char letter = alphabet[index];
                word += letter;
            }

            return ibanCountry + word + "00";
        }
        public UserBankInfo LogInUserInto1stTable(string card_number)
        {
            using (BankContext context = new BankContext())
            {         
                return context.UserBankInfos.FirstOrDefault(p => p.Card_number == card_number);
            }
        }
        public UserInfo LogInUserInto2ndTable(string egn)
        {
            using (BankContext context = new BankContext())
            {
                
                return context.UserInfos.FirstOrDefault(p => p.EGN == egn);
            }
        }
        public double Balance(UserBankInfo userBankInfo)
        {
            return userBankInfo.Balance;           
        }
        public void WithdrawDeposit(UserBankInfo userBankInfo)
        {
            
            using (BankContext context = new BankContext())
            {
                UserBankInfo user = LogInUserInto1stTable(userBankInfo.Card_number);

                user.Balance = userBankInfo.Balance;
                context.UserBankInfos.Update(user);
                context.SaveChanges();
            }
        }
        public double Transfer(UserBankInfo userBankInfo, string ibanReceiving, double transferAmount)
        {
            using (BankContext context = new BankContext())
            {
                userBankInfo = context.UserBankInfos.FirstOrDefault(p => p.IBAN == userBankInfo.IBAN);
                userBankInfo.Balance -= transferAmount;
                context.UserBankInfos.Update(userBankInfo);
                context.SaveChanges();

                UserBankInfo userReceiving = context.UserBankInfos.FirstOrDefault(p => p.IBAN == ibanReceiving);
                userReceiving.Balance += transferAmount;
                context.UserBankInfos.Update(userReceiving);
                context.SaveChanges();
            }
            return userBankInfo.Balance;
        }
        
    }
}
