#include "robot_package/robotic_man.h" 


int main(int argc, char **argv) {
    // Initialize the robotic node
    printf("Robotic node initialized.\n");

    robotic_node* robotic = new robotic_node(argc,argv);
    robotic->publishData();
    return 0;
}

