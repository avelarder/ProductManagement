using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Core
{
    public interface IRepository
    {
        void SetRepositoryStrategy(RepositoryStrategyEnum strategy);
        RepositoryStrategyEnum GetRepository();
        string GetTargetSettings();
    }

    public enum RepositoryStrategyEnum
    {
        InMemory,
        Database,
        XML,
        CSV
    }

}
