using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class DuckManager : Singleton<DuckManager>
    {
        public List<Duck> DuckList = new List<Duck>();

        public void CommandDuck(GameObject breadTarget)
        {
            foreach (var duck in DuckList.Where(duck => duck.CurrentState == Duck.State.Flying))
            {
                duck.PursueBread(breadTarget);
                break;
            }
        }
    }
}