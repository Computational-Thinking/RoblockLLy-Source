using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UBlockly
{
    [CodeInterpreter(BlockType = "sensors_touch_contact")]
    public class Detection_Sensor_Touch_Contact_Cmdtor : ValueCmdtor{
        protected override DataStruct Execute(Block block){
            string sensorPosition = block.GetFieldValue("POSITION");
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            bool data = selectedRobot.GetComponent<RobotManager>().GetTouchInfo(sensorPosition);
            return new DataStruct(data);
        }
    }

    [CodeInterpreter(BlockType = "sensors_touch_not_contact")]
    public class Detection_Sensor_Touch_Not_Contact_Cmdtor : ValueCmdtor{
        protected override DataStruct Execute(Block block){
            string sensorPosition = block.GetFieldValue("POSITION");
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            bool data = selectedRobot.GetComponent<RobotManager>().GetTouchInfo(sensorPosition);
            if(!data)
            {
                data = true;
            } else {
                data = false;
            }
            return new DataStruct(data);
        }
    }

    [CodeInterpreter(BlockType = "sensors_ir_detect_white")]
    public class Detection_Sensor_IR_White_Cmdtor : ValueCmdtor{
        protected override DataStruct Execute(Block block){
            string sensorPosition = block.GetFieldValue("POSITION");
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            bool data = selectedRobot.GetComponent<RobotManager>().GetIRInfo(sensorPosition, "White (Instance)");
            return new DataStruct(data);
        }
    }

    [CodeInterpreter(BlockType = "sensors_ir_detect_black")]
    public class Detection_Sensor_IR_Black_Cmdtor : ValueCmdtor{
        protected override DataStruct Execute(Block block){
            string sensorPosition = block.GetFieldValue("POSITION");
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            bool data = selectedRobot.GetComponent<RobotManager>().GetIRInfo(sensorPosition, "Black (Instance)");
            return new DataStruct(data);
        }
    }

    [CodeInterpreter(BlockType = "sensors_color_detect_colors")]
    public class Detection_Sensor_Color_Colors_Cmdtor : ValueCmdtor{
        protected override DataStruct Execute(Block block){
            string sensorPosition = block.GetFieldValue("POSITION");
            string colorToFind = block.GetFieldValue("COLOR");
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            bool data = selectedRobot.GetComponent<RobotManager>().GetColorInfo(sensorPosition, colorToFind);
            return new DataStruct(data);
        }
    }

    [CodeInterpreter(BlockType = "sensors_us_distance")]
    public class Detection_Sensor_US_Detection_Cmdtor : ValueCmdtor{
        protected override DataStruct Execute(Block block){
            string sensorPosition = block.GetFieldValue("POSITION");
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            float data = selectedRobot.GetComponent<RobotManager>().GetUSInfo(sensorPosition);
            return new DataStruct(data);
        }
    }
    
    [CodeInterpreter(BlockType = "sensors_gyroscope_still")]
    public class Detection_Sensor_Gyroscope_Still_Cmdtor : ValueCmdtor{
        protected override DataStruct Execute(Block block){
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            bool data = selectedRobot.GetComponent<RobotManager>().GetGyroscopeInfo("parallel");
            return new DataStruct(data);
        }
    }

    [CodeInterpreter(BlockType = "sensors_gyroscope_direction")]
    public class Detection_Sensor_Gyroscope_Direction_Cmdtor : ValueCmdtor{
        protected override DataStruct Execute(Block block){
            string robotDirection = block.GetFieldValue("DIRECTION");
            GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot");
            bool data = selectedRobot.GetComponent<RobotManager>().GetGyroscopeInfo(robotDirection);
            return new DataStruct(data);
        }
    }
}
