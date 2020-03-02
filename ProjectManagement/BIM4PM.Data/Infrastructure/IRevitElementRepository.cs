using BIM4PM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
   public interface IRevitElementRepository
    {
        IEnumerable<RevitElement> GetRevitElements();
        RevitElement GetRevitElement(int id);
        void Post(IEnumerable<RevitElement> revitElements);
        void Delete(IEnumerable<RevitElement> revitElements);
    }
}
