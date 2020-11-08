#include <Windows.h>
#include "BananaLoader.h"
#include "BananaLoader_Base.h"

BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpvReserved)
{
	BananaLoader::thisdll = hinstDLL;
	if (fdwReason == DLL_PROCESS_ATTACH)
	{
#ifndef DEBUG
		DisableThreadLibraryCalls(BananaLoader::thisdll);
#endif
		BananaLoader::Main();
	}
	else if (fdwReason == DLL_PROCESS_DETACH)
	{
		BananaLoader::UNLOAD();
		FreeLibrary(BananaLoader::thisdll);
	}
	return TRUE;
}