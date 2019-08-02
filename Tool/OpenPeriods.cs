using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITDB.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ITDB.Tool
{
    public class OpenPeriods
    {
        protected string ConnectionString { get; set; }
        public OpenPeriods() {
            ConnectionString = Environment.GetEnvironmentVariable("MySqlConnection");
        }
        public void WaitOpen(int pid)
        {
            Task task = new Task(() =>
            {
                //等待10分钟
                Thread.Sleep(10);
                using (DBContext _context = new DBContext(new DbContextOptionsBuilder<DBContext>(new DbContextOptions<DBContext>()).UseMySql(ConnectionString).Options))//ConnectionStrings->DefaultConnection
                {
                    var period = _context.DBPeriods.Find(pid);
                    try
                    {
                        Random random = new Random();
                        period.IfOpen = true;
                        period.LuckyCode = random.Next(0, period.NeedNum + 1);
                        period.OpenTime = DateTime.Now;
                        var luckyuser = _context.DBOrderDetails.FirstOrDefault(s => s.DBPeriodsID == period.ID && s.DBTicket == period.LuckyCode);
                        period.LuckyUserID = luckyuser.UserID;
                        period.Status = 2;//0 进行中 1正在开奖中2开奖成功3开奖失败
                        _context.SaveChanges();
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine("开奖失败："+e.Message);
                        period.Status = 3;//0 进行中 1正在开奖中2开奖成功3开奖失败
                        _context.SaveChanges();
                    }
                }



            });
            task.Start();
            task.ContinueWith((t) =>
            {
                Console.WriteLine("任务完成，完成时候的状态为：");
                Console.WriteLine("IsCanceled={0}\tIsCompleted={1}\tIsFaulted={2}",
                t.IsCanceled,
                t.IsCompleted,
                t.IsFaulted);
            });
        }
    }
}
