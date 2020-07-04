using MODEL.COMMON;
using MODEL.DTO;
using MODEL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.UTIL
{
    public static class AccountMapper
    {
        public static Account toAccount(AccountDTO accountDTO)
        {
            var account = new Account();
            account.PhoneNum = accountDTO.PhoneNum;
            account.Password = BcryptPass.HashPassword(accountDTO.Password);
            account.Fullname = accountDTO.Fullname;
            account.RoleId = 2;
            return account;
        }
        public static AccountDTO toDTO(Account account)
        {
            var accountDTO = new AccountDTO();
            accountDTO.AccountId = account.AccountId;
            accountDTO.Email = account.Email;
            accountDTO.Fullname = account.Fullname;
            accountDTO.Money = account.Money;
            accountDTO.PhoneNum = account.PhoneNum;
            accountDTO.RoleId = account.RoleId;
            return accountDTO;
        }
    }
}
