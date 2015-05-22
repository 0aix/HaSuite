﻿using MapleLib.WzLib.WzStructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor.Instance.Shapes
{
    public class Chair : MapleDot, ISnappable
    {
        public Chair(Board board, int x, int y)
            : base(board, x, y)
        {
        }

        public override bool CheckIfLayerSelected(SelectionInfo sel)
        {
            return true;
        }

        public override void DoSnap()
        {
            FootholdLine closestLine = null;
            double closestDistance = double.MaxValue;
            foreach (FootholdLine fh in Board.BoardItems.FootholdLines)
            {
                if (!fh.IsWall && BetweenOrEquals(X, fh.FirstDot.X, fh.SecondDot.X, (int)UserSettings.SnapDistance) && BetweenOrEquals(Y, fh.FirstDot.Y, fh.SecondDot.Y, (int)UserSettings.SnapDistance))
                {
                    double targetY = fh.CalculateY(X) - 1;
                    double distance = Math.Abs(targetY - Y);
                    if (closestDistance > distance) { closestDistance = distance; closestLine = fh; }
                }
            }
            if (closestLine != null) this.Y = (int)closestLine.CalculateY(X) - 1;
        }

        public override XNA.Color Color
        {
            get
            {
                return UserSettings.ChairColor;
            }
        }

        public override XNA.Color InactiveColor
        {
            get { return MultiBoard.ChairInactiveColor; }
        }

        public override ItemTypes Type
        {
            get { return ItemTypes.Chairs; }
        }

        protected override bool RemoveConnectedLines
        {
            get { return true; }
        }
    }
}
