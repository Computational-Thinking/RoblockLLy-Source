using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UBlockly
{
    [CodeInterpreter(BlockType = "move_robot_forward")]
    public class Move_Forward_Robot_Cmdtor : EnumeratorCmdtor{
        protected override IEnumerator Execute(Block block){
            string velocityString = block.GetFieldValue("VELOCITY");
            int velocityToMove = int.Parse(velocityString);
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
        
            yield return selectedRobot.GetComponent<RobotMotionController>().MoveVerticalRobotInfinite(velocityToMove * 10.0f, true);
        }
    }
    [CodeInterpreter(BlockType = "move_robot_backward")]
    public class Move_Backward_Robot_Cmdtor : EnumeratorCmdtor{
        protected override IEnumerator Execute(Block block){
            string velocityString = block.GetFieldValue("VELOCITY");
            int velocityToMove = int.Parse(velocityString);
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
        
            yield return selectedRobot.GetComponent<RobotMotionController>().MoveVerticalRobotInfinite(velocityToMove * 10.0f, false);
        }
    }

    [CodeInterpreter(BlockType = "move_robot_forward_time")]
    public class Move_Forward_Time_Robot_Cmdtor : EnumeratorCmdtor{
        protected override IEnumerator Execute(Block block){
            string velocityString = block.GetFieldValue("VELOCITY");
            int velocityToMove = int.Parse(velocityString);
            string timeString = block.GetFieldValue("TIME");
            float timeToMove = float.Parse(timeString);
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
        
            yield return selectedRobot.GetComponent<RobotMotionController>().MoveVerticalRobotTime(velocityToMove * 10.0f, timeToMove, true);
        }
    }

    [CodeInterpreter(BlockType = "move_robot_backward_time")]
    public class Move_Backward_Time_Robot_Cmdtor : EnumeratorCmdtor{
        protected override IEnumerator Execute(Block block){
            string velocityString = block.GetFieldValue("VELOCITY");
            int velocityToMove = int.Parse(velocityString);
            string timeString = block.GetFieldValue("TIME");
            int timeToMove = int.Parse(timeString);
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
        
            yield return selectedRobot.GetComponent<RobotMotionController>().MoveVerticalRobotTime(velocityToMove * 10.0f, timeToMove, false);
        }
    }

        [CodeInterpreter(BlockType = "move_turn_robot")]
        public class Turn_Robot_Cmdtor : EnumeratorCmdtor{
        protected override IEnumerator Execute(Block block)
        {
            //float angleToRotate = float.Parse(block.GetFieldValue("ANGLE"));
            string directionToRotate = block.GetFieldValue("DIRECTION");
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            yield return selectedRobot.GetComponent<RobotMotionController>().RotateRobot(90.0f, directionToRotate);
        }
    }

    [CodeInterpreter(BlockType = "move_turn_robot_angle")]
    public class Turn_Robot_Angle_Cmdtor : EnumeratorCmdtor{
        protected override IEnumerator Execute(Block block)
        {
            float angleToRotate = float.Parse(block.GetFieldValue("ANGLE"));
            string directionToRotate = block.GetFieldValue("DIRECTION");
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            yield return selectedRobot.GetComponent<RobotMotionController>().RotateRobot(angleToRotate, directionToRotate);
        }
    }

    [CodeInterpreter(BlockType = "move_stop_robot")]
    public class Stop_Robot_Cmdtor : EnumeratorCmdtor{
        protected override IEnumerator Execute(Block block)
        {
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            yield return selectedRobot.GetComponent<RobotMotionController>().StopRobot();
        }
    }

}
