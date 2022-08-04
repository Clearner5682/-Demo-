using System;
using System.Collections.Generic;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace OrderService
{
    public class OrderNumHelper:ISingletonDependency
    {
        private readonly IGuidGenerator guidGenerator;
        // 真实环境下可以将dic存在redis中，这样就不会丢了
        private Dictionary<string, IList<int>> dic = new Dictionary<string, IList<int>>();

        public OrderNumHelper(IGuidGenerator guidGenerator)
        {
            this.guidGenerator = guidGenerator;
        }

        public string GenerateOrderNum()
        {
            DateTime now = DateTime.Now;
            string dateString = now.ToString("yyyyMMddHHmmss");
            Guid guid = this.guidGenerator.Create();
            int hashCode = guid.GetHashCode();
            Random random = new Random(hashCode);
            int randomNum = random.Next(9999);

            while (IsRepeat(randomNum))
            {
                randomNum = random.Next(9999);
            }

            string orderNum = dateString;
            string randomNumStr = randomNum.ToString();
            while (randomNumStr.Length < 4)
            {
                randomNumStr ="0" + randomNumStr;
            }

            orderNum += randomNumStr;

            return orderNum;
        }

        private bool IsRepeat(int num)
        {
            var todayKey = DateTime.Now.ToString("yyyyMMdd");
            if (!dic.ContainsKey(todayKey))
            {
                dic.Add(todayKey, new List<int>() { num });
                return false;
            }
            if (dic[todayKey].Contains(num))
            {
                return true;
            }

            dic[todayKey].Add(num);

            return false;
        }
    }
}
