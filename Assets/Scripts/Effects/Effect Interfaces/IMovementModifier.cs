using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IMovementModifier
{
    MovementModify ModifyMovement(MovementModify movementModify);
}
