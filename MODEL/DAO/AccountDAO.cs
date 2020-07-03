using MODEL.COMMON;
using MODEL.DTO;
using MODEL.EF;
using MODEL.UTIL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DAO
{
    public class AccountDAO
    {
        private NTDbContext _context;
        public AccountDAO()
        {
            _context = new NTDbContext();
        }
        public AccountDTO FindByPhoneNumber(string phoneNumber)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.PhoneNum == phoneNumber);
            var accountDTO = AccountMapper.toDTO(account);
            return accountDTO;
        }
        public int Login(string phoneNumber, string password)
        {
            var user = _context.Accounts.FirstOrDefault(x => x.PhoneNum == phoneNumber);
            if(user == null)
            {
                return 0;
            }
            if(BcryptPass.ValidatePassword(password, user.Password))
            {
                return 1;
            }
            return -1;
        }
        public int Register(AccountDTO accountDTO)
        {
            var count = _context.Accounts.Count(x => x.PhoneNum == accountDTO.PhoneNum);
            if (count > 0)
            {
                return 0;
            }
            var account = AccountMapper.toAccount(accountDTO);
            try
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
