using System.Drawing;
using System.Collections.Generic;
using Domain.Models;


namespace UseCase.Move
{

    internal interface IMoveStrategy
    {

        List<Point> SeekMovePoints(Point point, int step);

    }  
    
}
