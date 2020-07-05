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
        public PagedResult<AccountDTO> GetAll(string phoneNumber, int page, int pageSize)
        {
            var query = from a in _context.Accounts
                        select new AccountDTO()
                        {
                            AccountId = a.AccountId,
                            Fullname = a.Fullname,
                            PhoneNum = a.PhoneNum,
                            Email = a.Email,
                            Money = a.Money,
                        };
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                query = query.Where(x => x.PhoneNum.Contains(phoneNumber));
            }
            var result = new PagedResult<AccountDTO>();
            result.TotalRecord = query.Count();
            result.Items = query.OrderBy(x => x.AccountId)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize).ToList();
            return result;
        }
        public int UpdatePassword(string phoneNumber, List<string> passwords)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.PhoneNum == phoneNumber);
            if(account != null)
            {
                if(BcryptPass.ValidatePassword(passwords[0], account.Password))
                {
                    account.Password = BcryptPass.HashPassword(passwords[1]);
                    try
                    {
                        _context.SaveChanges();
                        return 1;
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                }
                else
                {
                    return 0;
                }
            }
            return -1;
        }
        public int UpdateAccount(AccountDTO accountDTO)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.PhoneNum == accountDTO.PhoneNum);
            account.Fullname = accountDTO.Fullname;
            account.Email = accountDTO.Email;
            try
            {
                _context.SaveChanges();
                return 1;
            }
            catch(Exception)
            {
                return 0;
            }
        }
    }
}
