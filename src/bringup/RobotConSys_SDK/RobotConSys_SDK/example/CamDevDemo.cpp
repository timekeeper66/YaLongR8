#include "CamDev/CamDev_Client.h"

#include <stdio.h>
#include <vector>
#include <opencv2/opencv.hpp>

using namespace DevLayer;

#define IMG_COLOR

int main(int argc, char* argv[]){
    CamDev_Client cam;
    if(cam.init("192.168.139.128", 8070) == 0){
        printf("cam init success\n");
    }
    else{
        printf("cam init failed\n");
        return 1;
    }

    cv::Mat img;
    uint8_t imgBuf[IMG_BUF_LEN];
    int len;
    while(1){
#ifdef IMG_COLOR
        len = cam.capture(imgBuf, CAM_DEV_IMG_TYPE_COLOR);
#else
        len = cam.capture(imgBuf, CAM_DEV_IMG_TYPE_DEPTH);
#endif
        printf("len = %d\n", len);
        if(len < 0){
            exit(1);
        }

        std::vector<uint8_t> img_bytes(imgBuf+IMGHEAD.length(), imgBuf+IMGHEAD.length()+len);
#ifdef IMG_COLOR        
        img = cv::imdecode(img_bytes, cv::IMREAD_COLOR);
#else
        img = cv::imdecode(img_bytes, cv::IMREAD_ANYDEPTH);
#endif

        printf("row = %d, col = %d\n", img.rows, img.cols);
        cv::imshow("img", img);
        int key = cv::waitKey(0);
        if(key == 'q'){
            break;
        }
    }

    cam.close();
}
