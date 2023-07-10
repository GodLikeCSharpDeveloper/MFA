using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Utility.UiHelper.CollectionUiLogic
{
    public interface ICollectionUiLogic<T>
    {
        public ObservableCollection<T> OnCollectionEndReached(List<T> fullList, ObservableCollection<T> list,ref int count);
    }
}
