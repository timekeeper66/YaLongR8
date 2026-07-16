#pragma once
#include <stdio.h>
#include <iostream>
#include <string>

#ifdef __linux__
#include <dlfcn.h>
#include <unistd.h>
#include <libgen.h>
#else
#include <Windows.h>
#endif // !WIN32

using namespace std;

class CLoadLibrary
{
public:
	CLoadLibrary();
	~CLoadLibrary();

	bool OpenLibrary(string strPath);

	template <typename T>
	T GetFunction(const char* strName) {
		T pFun = NULL;
		do {
#ifndef __linux__
			if (handle == NULL) {
				printf("LoadLibrary False\n");
				break;
			}

			pFun = (T)GetProcAddress(handle, strName);
			if (!pFun) {
				printf("GetProcAddress False:%s\n",strName);
			}
#else
			char *err = NULL;
			if (!handle) {
				printf("LoadLibrary False\n");
				break;
			}

			//clear error info
			dlerror();

			pFun = (T)dlsym(handle, strName);
			err = dlerror();
			if (!pFun || err) {
				printf("GetProcAddress False:%d,%s\n",errno,err);
			}else{
				printf("GetProcAddress succuse:%s\n",strName);
			}
#endif // WIN32
		} while (false);

		return pFun;
}

private:
#ifndef __linux__
	HMODULE handle = nullptr;
#else
	void * handle = nullptr;
#endif
};


std::string GetModuleFullPath ( int bLastPath );
