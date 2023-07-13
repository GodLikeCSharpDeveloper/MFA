using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Utility.UiHelper.CollectionUiLogic
{
    public class CollectionUiLogic<T> : ICollectionUiLogic<T>
    {
        public ObservableCollection<T> OnCollectionEndReached(List<T> fullList, ObservableCollection<T> list, ref int count)
        {
            
            if (count < fullList.Count)
            {
                var allTopics = fullList.Skip(count).Take(10);
                foreach (var item in allTopics)
                    list.Add(item);
                count += 10;
                return list;
            }
            
            return list;
        }
    }
}
