using System;
using System.Collections.Generic;
using System.Text;

namespace BiddingEngineAPI.EFCore.DAL
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
