#include "robot_package/loard_library.h"
#include <mutex>
#include <functional>
#include <string.h>


#ifndef MAX_PATH
#define MAX_PATH 256
#endif



CLoadLibrary::CLoadLibrary()
{
}

CLoadLibrary::~CLoadLibrary()
{
	if(handle!=nullptr)
#ifdef WIN32
	::FreeLibrary(handle);
#else
	dlclose(handle);
#endif // WIN32
	handle = nullptr;
}

bool CLoadLibrary::OpenLibrary(string strPath) {
	bool bRt = false;
#ifndef __linux__
		handle = ::LoadLibrary(strPath.c_str());
		if (handle == nullptr) {
			printf("LoadLibrary False:%s\n" , strPath.c_str());
			printf("errno :%d\n",errno);
		}else{
			printf("LoadLibrary success:%s\n" , strPath.c_str());
			bRt=true;
		}
#else
		char *err = nullptr;
		dlerror();
		handle = dlopen(strPath.c_str(), RTLD_NOW);
		if (!handle) {
			printf("LoadLibrary False:%s\n" , strPath.c_str());
			printf("LoadLibrary False errno:%d, %s\n" , errno, err);
		}else{
				printf("LoadLibrary success :%s \r\n " , strPath.c_str());
				bRt=true;
		}

#endif // WIN32

	return bRt;
}



std::string GetModuleFullPath ( int bLastPath )
{
	char chPath [MAX_PATH] = {0};

#ifndef __linux__
	//::GetModuleFileName(NULL, chPath, sizeof(chPath));
	HMODULE hLocalMoudle = NULL;
	::GetModuleHandleEx(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS, (LPCTSTR)(&GetModuleFullPath), &hLocalMoudle);
	::GetModuleFileNameA(hLocalMoudle, (LPSTR)chPath, MAX_PATH);
#else
    memset(chPath, 0, sizeof(chPath));
    ::readlink("/proc/self/exe", chPath, sizeof (chPath));
#endif
	std::string strModuleFileName = chPath;

	if ( bLastPath == 0 )
	{
		return strModuleFileName.c_str();
	}

    //如果文件名称有生僻汉字并且第二字节是'\\'时，用find_last_of的方法就会有问题
    //比如："c:\\ABC癨.txt"就会返回"c:\\ABC\\"，而用_splitpath和_makepath正确返回"c:\\"
//	size_t nPos = strModuleFileName.find_last_of(PATH_SEP_STRING);
//	std::string strLastPath = strModuleFileName.substr(0, nPos);

#ifndef __linux__
	char szDrive[_MAX_DRIVE];
	char szDir[_MAX_DIR];
	::_splitpath (chPath, szDrive, szDir, NULL, NULL);
	::_makepath (chPath, szDrive, szDir, NULL, NULL);
	//去除最后的'\\'
	if (chPath[strlen(chPath) - 1] == '\\')
	{
		chPath[strlen(chPath) - 1] = '\0';
	}

#else
    ::dirname(chPath);
#endif

    std::string strLastPath = chPath;
	return strLastPath.c_str();
}
