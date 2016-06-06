using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Wpf_ObjectDataProvider
{
    public class Members 
    {
        private ObservableCollection<Member> _members = new ObservableCollection<Member>
        {
            new Member{ Id =  1 , Name = "Ouch Liu" , Sex = true },
            new Member{ Id =  2 , Name = "Yuan Monkey" , Sex = false },
            new Member{ Id =  3 , Name = "Bill Gates" , Sex = true },
            new Member{ Id =  4 , Name = "Angela Baby" , Sex = false },
            new Member{ Id =  5 , Name = "Wei Liu" , Sex = true },
        };

        public List<Member> GetFemales()
        {
            return _members.Where( m => m.Sex == false ).ToList();
        }

        public List<Member> GetMales()
        {
            return _members.Where( m => m.Sex == true ).ToList();

        }
    }
}
