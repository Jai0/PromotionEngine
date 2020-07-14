using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public class Program
    {        
        static void Main(string[] args)
        {

            int count = 0;
            Promotion p = new Promotion();
            count = p.PromotionValue();
            Console.WriteLine("The calcualte promotionn value is: "+count);
            Console.ReadKey();


        }
        
    }

    public class Promotion
    {
        public  int PromotionValue()
        {
            List<Tuple<string, int, int>> rule = new List<Tuple<string, int, int>>();
            rule = PromotionRules();
            Dictionary<string, int> skuList = new Dictionary<string, int>();
            skuList = SKUTable();
            int totalValue = 0;
            List<SKUModel> scenarios = new List<SKUModel>();
            scenarios = PromotionScenario();
            int cNum = 0;
            int cValue = 0;
            int cTotalValue = 0;
            foreach (SKUModel scenario in scenarios)
            {
                List<Tuple<string, int, int>> finalkey = rule.Where(a => a.Item1.Contains(scenario.Id)).ToList();
                int ruleCount = finalkey[0].Item2;
                int ruleValue = finalkey[0].Item3;
                string skuKey = skuList.Where(b => b.Key.Contains(scenario.Id)).ToList()[0].Key;
                int skuVal = skuList.Where(b => b.Key.Contains(scenario.Id)).ToList()[0].Value;


                if (skuKey == "C")
                {
                    cNum = scenario.Number;
                    cValue = skuVal;
                    cTotalValue = scenario.Number * skuVal;
                    totalValue += cTotalValue;
                    continue;
                }
                if (skuKey == "D" && cNum > 0)
                {
                    int loop = 0;
                    int remCount = 0;
                    if (cNum > scenario.Number)
                    {
                        loop = scenario.Number;
                        remCount = cNum - scenario.Number;
                        totalValue += remCount * cValue;

                    }
                    else if (cNum < scenario.Number)
                    {
                        loop = cNum;
                        remCount = scenario.Number - cNum;
                        totalValue += remCount * skuVal;

                    }
                    else
                    {
                        loop = cNum;
                    }
                    for (int i = 0; i < loop; i++)
                    {
                        totalValue += ruleValue;
                    }

                    totalValue = totalValue - cTotalValue;
                    cNum = 0;
                }                
                else
                {
                    int quo = scenario.Number / ruleCount;
                    int rem = scenario.Number % ruleCount;

                    totalValue += quo * ruleValue + rem * skuVal;
                }

            }

            return totalValue;
        }
        public List<SKUModel> PromotionScenario()
        {
            List<SKUModel> lst = new List<SKUModel>();
            Console.WriteLine("Please enter Scenario legnth");
            int skuLength = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < skuLength; i++)
            {
                SKUModel sku = new SKUModel();
                Console.WriteLine("Please enter SKU" + i + " name and Number");
                sku.Id = Console.ReadLine();
                sku.Number = Convert.ToInt32(Console.ReadLine());
                lst.Add(sku);

            }
            return lst;
        }
        public List<Tuple<string, int, int>> PromotionRules()
        {
            List<Tuple<string, int, int>> rule = new List<Tuple<string, int, int>>();
            rule.Add(new Tuple<string, int, int>("A", 3, 130));
            rule.Add(new Tuple<string, int, int>("B", 2, 45));
            rule.Add(new Tuple<string, int, int>("CD", 1, 30));
            return rule;
        }
        public Dictionary<string, int> SKUTable()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            Console.WriteLine("Please enter SKU legnth");
            int skuLength = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < skuLength; i++)
            {
                Console.WriteLine("Please enter SKU" + i + " name and price");
                string id = Console.ReadLine();
                int price = Convert.ToInt32(Console.ReadLine());
                dict.Add(id, price);

            }
            return dict;

        }
    }

}
