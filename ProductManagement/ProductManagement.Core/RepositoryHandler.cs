using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Core
{
    public class RepositoryHandler : IRepository
    {
        RepositoryStrategyEnum currentStrategy;
        
        public RepositoryHandler()
        {
        }

        public RepositoryStrategyEnum GetRepository() 
        {
            return currentStrategy;
        }

        public string GetTargetSettings()
        {
            string target = string.Empty;
            switch (currentStrategy)
            {
                case RepositoryStrategyEnum.Database:
                    target = System.Configuration.ConfigurationManager.AppSettings["persistence:database"];
                    break;
                case RepositoryStrategyEnum.XML:
                    target = System.Configuration.ConfigurationManager.AppSettings["persistence:xml"];
                    break;
                case RepositoryStrategyEnum.CSV:
                    target = System.Configuration.ConfigurationManager.AppSettings["persistence:csv"];
                    break;
            }
            return target;
        }

        public void SetRepositoryStrategy(RepositoryStrategyEnum strategy)
        {
            

            currentStrategy = strategy;
        }
    }
}
