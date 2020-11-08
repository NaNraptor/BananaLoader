#pragma once
#include "Mono.h"

class BananaLoader_Base
{
public:
	enum BananaCompatibility
	{
		UNIVERSAL,
		COMPATIBLE,
		NOATTRIBUTE,
		INCOMPATIBLE
	};

	static bool HasInitialized;
	static MonoMethod* startup;
	
	static void Initialize();
	static void Startup();
};