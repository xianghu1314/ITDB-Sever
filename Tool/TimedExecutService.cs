using ITDB.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ITDB.Tool
{
    /// <summary>
    /// 简单的定时任务执行
    /// </summary>
    public class TimedExecutService : BackgroundService
    {
        protected readonly DBContext _context;
        public ILogger _logger;
        public TimedExecutService(ILogger<TimedExecutService> logger)
        {
            this._logger = logger;
            var _connectionString = Environment.GetEnvironmentVariable("MySqlConnection");
            this._context = new DBContext(new DbContextOptionsBuilder<DBContext>(new DbContextOptions<DBContext>()).UseMySql(_connectionString).Options);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation(DateTime.Now.ToString() + "后台开奖服务：启动");

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(5000, stoppingToken); //启动后5秒执行一次 (用于测试)
                    //1.获取待开奖列表
                    //2.获取彩票结果
                    //3.计算中奖结果
                    //4.保存开奖信息
                    using (var tran = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            //1.获取待开奖列表
                            var now = DateTime.Now;
                            var _DBPeriod = await _context.DBPeriods.Where(s=>s.WaitOpenTime<= now).FirstOrDefaultAsync(s => s.Status == 1);
                            if (_DBPeriod == null)
                            {
                                _logger.LogInformation("后台开奖服务：暂无开奖期数");
                                tran.Rollback();
                                return;
                            }
                            //var _PeriodDetails = _context.DBOrderDetails.Where(s => s.DBPeriodsID == _DBPeriod.ID).Select(s=>new { s.DBTicket }).ToArray();
                            //2.获取彩票结果
                            Random random = new Random();
                            //3.计算中奖结果
                            int LuckyCode = random.Next(0, _DBPeriod.NeedNum+1);//0到length-1
                            //4.保存开奖信息
                            var luckyuser = _context.DBOrderDetails.FirstOrDefault(s => s.DBPeriodsID == _DBPeriod.ID && s.DBTicket == LuckyCode);
                            _DBPeriod.LuckyCode = LuckyCode;
                            _DBPeriod.LuckyUserID = luckyuser.UserID;
                            _DBPeriod.OpenTime = now;
                            _DBPeriod.Status = 2;//0 进行中 1正在开奖中2开奖成功3开奖失败
                            _context.SaveChanges();
                            //提交
                            tran.Commit();
                        }
                        catch (Exception ex)//自动Rollback
                        {
                            Console.WriteLine("后台开奖服务,开奖失败：" + ex.Message);
                            //提交
                        }
                        
                    }
                }
                _logger.LogInformation(DateTime.Now.ToString() + "后台开奖服务：停止");
            }
            catch (Exception ex)            {
                _logger.LogInformation(DateTime.Now.ToString() + "后台开奖服务：异常" + ex.Message + ex.StackTrace);
            }
        }
    }
}