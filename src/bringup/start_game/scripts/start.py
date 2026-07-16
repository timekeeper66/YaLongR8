#!/usr/bin/python
# coding: utf-8


# 313
import rospy
import sys
from geometry_msgs.msg import Twist
from std_msgs.msg import String
import time

def run_mode11():        
        rospy.loginfo("+++++++++++[   move to  1   ]+++++++++++")
        ctl.move(0.5,0,2.8)

        time.sleep(1.0)
        
        ctl.move(-0.5,0,5.0)
        time.sleep(45.0)
        ctl.move(0,-0.5,5.3)
        
        time.sleep(1.0)
        
        ctl.move(0.8,0,5.0)
        
        time.sleep(1.0)
        
        ctl.move(0,0.5,1.0)        
        time.sleep(5.0)        
        ctl.move(0,-0.5,1.0)
        time.sleep(1.0)
        ctl.move(-0.5,0,8.0)
        time.sleep(1.0)
        ctl.move(0,0.5,5.3)
        time.sleep(1.0)
        ctl.move(0.5,0,2.2)
        
        time.sleep(1.0)
        
def run_mode12():        
        rospy.loginfo("+++++++++++[   move to  1   ]+++++++++++")
        ctl.move(0.5,0,2.8)
        time.sleep(1.0)        
        ctl.move(-0.5,0,5.0)
        time.sleep(45.0)        
        ctl.move(0.5,0,2.2) #back to centre
        
        ctl.move(0,-0.5,4.6)
        
        time.sleep(0.5)
        
        ctl.move(0.5,0,7.8)        
        time.sleep(0.5)        
        ctl.move(0,-0.5,4.6)
        time.sleep(0.5)
        ctl.move(0.5,0,4.0)
        time.sleep(0.5)
        ctl.move(0,0.5,4.6)
        time.sleep(5.0)
        #back
        ctl.move(0,-0.5,4.6)
        time.sleep(1.0)
        ctl.move(-0.5,0,4.0)
        time.sleep(1.0)
        ctl.move(0,0.5,4.6)
        time.sleep(1.0)
        ctl.move(-0.5,0,7.2)
        time.sleep(1.0)
        ctl.move(0,0.5,4.6)
        
        
def run_mode21():  
        rospy.loginfo("+++++++++++[   move to  2   ]+++++++++++")
        ctl.move(0.5,0,2.8)

        time.sleep(1.0)
        
        ctl.move(-0.5,0,5.0)
        time.sleep(45.0)
        ctl.move(0,-0.5,2.7)
        
        time.sleep(1.0)
        
        ctl.move(0.5,0,9.5)
        
        time.sleep(1.0)
        
        ctl.move(0,-0.5,2.0)        
        time.sleep(0.5)        
        ctl.move(0,0.5,2.0)
        time.sleep(5.0)
        ctl.move(-0.5,0,9.5)
        time.sleep(0.5)
        ctl.move(0,0.5,2.7)
        time.sleep(0.5)
        ctl.move(0.5,0,2.2)
        
        time.sleep(1.0)
        
def run_mode22():  
        rospy.loginfo("+++++++++++[   move to  2   ]+++++++++++")
        ctl.move(0.5,0,2.8)

        time.sleep(1.0)
        
        ctl.move(-0.5,0,5.0)
        time.sleep(45.0)
        ctl.move(0.5,0,2.2) #back to centre
        time.sleep(1.0)
        ctl.move(0,-0.5,4.6)#turn right 90
        time.sleep(1.0)
        ctl.move(0.5,0,7.8)
        time.sleep(1.0)
        ctl.move(0,0.5,4.6)#turn left 90
        time.sleep(1.0)
        ctl.move(0.5,0,3.0)
        time.sleep(1.0)
        ctl.move(0,-0.5,4.6)#turn right 90
        time.sleep(1.0)
        ctl.move(0,0.5,4.6)#turn left 90
        time.sleep(1.0)
        ctl.move(-0.5,0,2.6)
        time.sleep(1.0)
        ctl.move(0,-0.5,4.6)#turn right 90
        time.sleep(1.0)
        ctl.move(-0.5,0,7.2)
        time.sleep(1.0)
        ctl.move(0,0.5,4.6)#turn left 90
        
def run_mode23():  
        rospy.loginfo("+++++++++++[   move to  2   ]+++++++++++")
        ctl.move(0.5,0,2.8)

        time.sleep(1.0)
        
        ctl.move(-0.5,0,5.0)
        time.sleep(45.0)
        ctl.move(0,-0.5,5.3)
        
        time.sleep(1.0)
        
        ctl.move(0.5,0,8.0)
        
        time.sleep(1.0)
        
        ctl.move(0,0.5,5.0)        
        time.sleep(1.0)    
        ctl.move(0.5,0,6.8)
        time.sleep(1.0)
        
        ctl.move(-0.5,0,6.8)
        time.sleep(1.0)
        ctl.move(0,-0.5,5.0)
        time.sleep(1.0)
        ctl.move(-0.5,0,8.0)
        time.sleep(1.0)
        ctl.move(0,0.5,5.3)
        time.sleep(1.0)
        ctl.move(0.5,0,2.2)
        

        
        time.sleep(1.0)
        

        
        
def run_mode31():  
        rospy.loginfo("+++++++++++[   move to  3   ]+++++++++++")
        ctl.move(0.5,0,2.8)

        time.sleep(1.0)
        
        ctl.move(-0.5,0,5.0)
        time.sleep(45.0)
        ctl.move(0,0.5,2.5)
        
        time.sleep(0.5)
        
        ctl.move(0.5,0,9.1)
        
        time.sleep(0.5)
        
        ctl.move(0,0.5,2.0)        
        time.sleep(5.0)        
        ctl.move(0,-0.5,2.0)
        time.sleep(0.5)
        ctl.move(-0.5,0,9.1)
        time.sleep(0.5)
        ctl.move(0,-0.5,2.5)
        time.sleep(0.5)
        ctl.move(0.5,0,2.2)
        
        time.sleep(1.0)
        
def run_mode32():  
        rospy.loginfo("+++++++++++[   move to  3   ]+++++++++++")
        ctl.move(0.5,0,2.8)

        time.sleep(1.0)
        
        ctl.move(-0.5,0,5.0)
        time.sleep(45.0)
        ctl.move(0.5,0,2.2) #back to centre
        time.sleep(1.0)
        ctl.move(0,0.5,4.6)#turn left 90
        time.sleep(1.0)
        ctl.move(0.5,0,7.0)
        time.sleep(1.0)
        ctl.move(0,-0.5,4.6)#turn right 90
        time.sleep(1.0)
        ctl.move(0.5,0,3.8)
        time.sleep(1.0)
        ctl.move(0,0.5,4.6)#turn left 90
        time.sleep(1.0)
        ctl.move(0,-0.5,4.6)#turn right 90
        time.sleep(1.0)
        ctl.move(-0.5,0,3.8)
        time.sleep(1.0)
        ctl.move(0,0.5,4.6)#turn left 90
        time.sleep(1.0)
        ctl.move(-0.5,0,7.2)
        time.sleep(1.0)
        ctl.move(0,-0.5,4.6)#turn right 90
        
def run_mode33():  
        rospy.loginfo("+++++++++++[   move to  3   ]+++++++++++")
        ctl.move(0.5,0,2.8)

        time.sleep(1.0)
        
        ctl.move(-0.5,0,5.0)
        time.sleep(45.0)
        ctl.move(0,0.5,5.3)
        
        time.sleep(1.0)
        
        ctl.move(0.5,0,7.5)
        
        time.sleep(1.0)
        
        ctl.move(0,-0.5,5.5)        
        time.sleep(1.0)    
        ctl.move(0.5,0,7.5)
        time.sleep(1.0)
        ctl.move(0,0.5,4.6)#turn left 90
        time.sleep(1.0)
        ctl.move(0,-0.5,4.6)#turn right 90
        time.sleep(1.0)
        
        ctl.move(-0.5,0,7.5)
        time.sleep(1.0)
        ctl.move(0,0.5,5.5)
        time.sleep(1.0)
        ctl.move(-0.5,0,7.5)
        time.sleep(1.0)
        ctl.move(0,-0.5,5.3)
        time.sleep(1.0)
        ctl.move(0.5,0,2.2)
        
        time.sleep(1.0)
 

def run_mode41():      
        rospy.loginfo("+++++++++++[   move to  4   ]+++++++++++")
        ctl.move(0.5,0,2.8)

        time.sleep(1.0)
        
        ctl.move(-0.5,0,5.0)
        time.sleep(45.0)
        ctl.move(0,0.5,5.3)
        
        time.sleep(0.5)
        
        ctl.move(0.5,0,7.5)
        
        time.sleep(0.5)
        
        ctl.move(0,-0.5,1.0)        
        time.sleep(5.0)        
        ctl.move(0,0.5,1.0)
        time.sleep(1.0)
        ctl.move(-0.5,0,7.5)
        time.sleep(1.0)
        ctl.move(0,-0.5,5.3)
        time.sleep(1.0)
        ctl.move(0.5,0,2.2)
        
        time.sleep(1.0)        

        
def run_mode42():        
        rospy.loginfo("+++++++++++[   move to  4   ]+++++++++++")
        ctl.move(0.5,0,2.8)
        time.sleep(1.0)        
        ctl.move(-0.5,0,5.0)
        time.sleep(45.0)        
        ctl.move(0.5,0,2.2) #back to centre
        
        ctl.move(0,0.5,4.6)
        
        time.sleep(1.0)
        
        ctl.move(0.5,0,7.5)        
        time.sleep(1.0)        
        ctl.move(0,0.5,4.6)
        time.sleep(1.0)
        ctl.move(0.5,0,3.2)
        time.sleep(1.0)
        ctl.move(0,-0.5,4.6)
        time.sleep(1.0)
        #back
        ctl.move(0,0.5,4.6)
        time.sleep(1.0)
        ctl.move(-0.5,0,3.35)
        time.sleep(1.0)
        ctl.move(0,-0.5,4.6)
        time.sleep(1.0)
        ctl.move(-0.5,0,7.8)
        time.sleep(1.0)
        ctl.move(0,-0.5,4.6)
        
        
class Controller:
    def __init__(self):
#        rospy.init_node('start_game',anonymous=True)
        rospy.init_node('start_game')
        self.velocity_publisher = rospy.Publisher('/cmd_vel',Twist,queue_size=10)
        self.velocity_pub = rospy.Publisher('/car_fly',String,queue_size=10)
        self.rate = rospy.Rate(10)
        self.vel_msg = Twist()
        self.fly_msg = "fly"
        self.land_msg = "land"
        self.status = False
    def move(self,linear_x ,angular_z , duration):
        self.vel_msg.linear.x = linear_x
        self.vel_msg.linear.y = 0
        self.vel_msg.linear.z = 0
        self.vel_msg.angular.x = 0
        self.vel_msg.angular.y = 0
        self.vel_msg.angular.z = angular_z
        it = int(duration*10)
        for i in range(it):
            self.velocity_publisher.publish(self.vel_msg)
            self.rate.sleep()

        self.stop_move()
    def stop_move(self):
        self.vel_msg.linear.x = 0
        self.vel_msg.angular.z = 0
        self.velocity_publisher.publish(self.vel_msg)
        rospy.loginfo("+++++++++++[Robot Stopped]+++++++++++")
        
    

        
        

if __name__ == "__main__":

#  mode = rospy.get_param("mode", 1)
    
    
  try:
    ctl = Controller()
    
    mode = rospy.get_param("~mode", 1)
        
          

    if mode==11:
      run_mode11()
    elif mode==12:
      run_mode12()
    elif mode==21:
      run_mode21()
    elif mode==22:
      run_mode22()
    elif mode==23:
      run_mode23()
    elif mode==31:
      run_mode31()
    elif mode==32:
      run_mode32()
    elif mode==33:
      run_mode33()
    elif mode==41:
      run_mode41()
    elif mode==42:
      run_mode42()




    time.sleep(1)
    ctl.velocity_pub.publish(ctl.land_msg)

  except rospy.ROSInterruptException:
    rospy.loginfo("fail")
