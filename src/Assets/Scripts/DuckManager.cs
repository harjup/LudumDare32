using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class DuckManager : Singleton<DuckManager>
    {
        public List<BreadPiece> BreadList = new List<BreadPiece>();
        public List<Duck> DuckList = new List<Duck>();

        public void CommandDuck(GameObject breadTarget)
        {
            var duck = DuckList.First();
            DuckList.RemoveAt(0);
            duck.PursueBread(breadTarget);
        }
    }
}