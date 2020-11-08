#include "Exports.h"
#include "BananaLoader.h"
#include "Il2Cpp.h"
#include "Mono.h"
#include "HookManager.h"
#include "Logger.h"
#include "AssertionManager.h"

void Log(MonoString* namesection, MonoString* txt) { Logger::Log(Mono::mono_string_to_utf8(namesection), Mono::mono_string_to_utf8(txt)); }
void LogColor(MonoString* namesection, MonoString* txt, ConsoleColor color) { Logger::Log(Mono::mono_string_to_utf8(namesection), Mono::mono_string_to_utf8(txt), color); }
void LogWarning(MonoString* namesection, MonoString* txt) { Logger::LogWarning(Mono::mono_string_to_utf8(namesection), Mono::mono_string_to_utf8(txt)); }
void LogError(MonoString* namesection, MonoString* txt) { Logger::LogError(Mono::mono_string_to_utf8(namesection), Mono::mono_string_to_utf8(txt)); }
void LogBananaError(MonoString* namesection, MonoString* txt) { Logger::LogBananaError(Mono::mono_string_to_utf8(namesection), Mono::mono_string_to_utf8(txt)); }
void LogBananaCompatibility(BananaLoader_Base::BananaCompatibility comp) { Logger::LogBananaCompatibility(comp); }
bool IsIl2CppGame() { return BananaLoader::IsGameIl2Cpp; }
bool IsDebugMode() { return BananaLoader::DebugMode; }
bool IsConsoleEnabled() { return Console::Enabled; }
bool ShouldShowGameLogs() { return Console::ShouldShowGameLogs; }
MonoString* GetGameDirectory() { return Mono::mono_string_new(Mono::Domain, BananaLoader::GamePath); }
MonoString* GetGameDataDirectory() { return Mono::mono_string_new(Mono::Domain, BananaLoader::DataPath); }
void Hook(Il2CppMethod* target, void* detour) { HookManager::Hook(target, detour); }
void Unhook(Il2CppMethod* target, void* detour) { HookManager::Unhook(target, detour); }
bool IsOldMono() { return Mono::IsOldMono; }
MonoString* GetCompanyName() { return Mono::mono_string_new(Mono::Domain, ((BananaLoader::CompanyName == NULL) ? "UNKNOWN" : BananaLoader::CompanyName)); }
MonoString* GetProductName() { return Mono::mono_string_new(Mono::Domain, ((BananaLoader::ProductName == NULL) ? "UNKNOWN" : BananaLoader::ProductName)); }
MonoString* GetAssemblyDirectory() { return Mono::mono_string_new(Mono::Domain, Mono::AssemblyPath); }
MonoString* GetMonoConfigDirectory() { return Mono::mono_string_new(Mono::Domain, Mono::ConfigPath); }
MonoString* GetExePath() { return Mono::mono_string_new(Mono::Domain, BananaLoader::ExePath); }
bool IsQuitFix() { return BananaLoader::QuitFix; }
BananaLoader::LoadMode GetLoadMode_Plugins() { return BananaLoader::LoadMode_Plugins; }
BananaLoader::LoadMode GetLoadMode_Mods() { return BananaLoader::LoadMode_Mods; }
bool AG_Force_Regenerate() { return BananaLoader::AG_Force_Regenerate; }
MonoString* AG_Force_Version_Unhollower() { if (BananaLoader::ForceUnhollowerVersion != NULL) return Mono::mono_string_new(Mono::Domain, BananaLoader::ForceUnhollowerVersion); return NULL; }
void SetTitleForConsole(MonoString* txt) { Console::SetTitle(Mono::mono_string_to_utf8(txt)); }
void ThrowInternalError(MonoString* txt) { AssertionManager::ThrowInternalError(Mono::mono_string_to_utf8(txt)); }
HANDLE GetConsoleOutputHandle() { return Console::OutputHandle; }

void Exports::AddInternalCalls()
{
	Mono::mono_add_internal_call("BananaLoader.Imports::IsIl2CppGame", IsIl2CppGame);
	Mono::mono_add_internal_call("BananaLoader.Imports::IsDebugMode", IsDebugMode);
	Mono::mono_add_internal_call("BananaLoader.Imports::GetGameDirectory", GetGameDirectory);
	Mono::mono_add_internal_call("BananaLoader.Imports::GetGameDataDirectory", GetGameDataDirectory);
	Mono::mono_add_internal_call("BananaLoader.Imports::GetAssemblyDirectory", GetAssemblyDirectory);
	Mono::mono_add_internal_call("BananaLoader.Imports::GetMonoConfigDirectory", GetMonoConfigDirectory);
	Mono::mono_add_internal_call("BananaLoader.Imports::Hook", Hook);
	Mono::mono_add_internal_call("BananaLoader.Imports::Unhook", Unhook);
	Mono::mono_add_internal_call("BananaLoader.Imports::GetCompanyName", GetCompanyName);
	Mono::mono_add_internal_call("BananaLoader.Imports::GetProductName", GetProductName);
	Mono::mono_add_internal_call("BananaLoader.Imports::GetExePath", GetExePath);

	Mono::mono_add_internal_call("BananaLoader.BananaLoaderBase::IsOldMono", IsOldMono);
	Mono::mono_add_internal_call("BananaLoader.BananaLoaderBase::IsQuitFix", IsQuitFix);
	Mono::mono_add_internal_call("BananaLoader.BananaLoaderBase::UNLOAD", BananaLoader::UNLOAD);

	Mono::mono_add_internal_call("BananaLoader.BananaHandler::GetLoadMode_Plugins", GetLoadMode_Plugins);
	Mono::mono_add_internal_call("BananaLoader.BananaHandler::GetLoadMode_Mods", GetLoadMode_Mods);

	Mono::mono_add_internal_call("BananaLoader.BananaConsole::Allocate", Console::Create);
	Mono::mono_add_internal_call("BananaLoader.BananaConsole::SetTitle", SetTitleForConsole);
	Mono::mono_add_internal_call("BananaLoader.BananaConsole::SetColor", Console::SetColor);
	Mono::mono_add_internal_call("BananaLoader.BananaConsole::IsConsoleEnabled", IsConsoleEnabled);

	Mono::mono_add_internal_call("BananaLoader.BananaLogger::Native_Log", Log);
	Mono::mono_add_internal_call("BananaLoader.BananaLogger::Native_LogColor", LogColor);
	Mono::mono_add_internal_call("BananaLoader.BananaLogger::Native_LogWarning", LogWarning);
	Mono::mono_add_internal_call("BananaLoader.BananaLogger::Native_LogError", LogError);
	Mono::mono_add_internal_call("BananaLoader.BananaLogger::Native_LogBananaError", LogBananaError);
	Mono::mono_add_internal_call("BananaLoader.BananaLogger::Native_LogBananaCompatibility", LogBananaCompatibility);
	Mono::mono_add_internal_call("BananaLoader.BananaLogger::Native_ThrowInternalError", ThrowInternalError);
	Mono::mono_add_internal_call("BananaLoader.BananaLogger::Native_GetConsoleOutputHandle", GetConsoleOutputHandle);

	Mono::mono_add_internal_call("BananaLoader.AssemblyGenerator::Force_Regenerate", AG_Force_Regenerate);
	Mono::mono_add_internal_call("BananaLoader.AssemblyGenerator::Force_Version_Unhollower", AG_Force_Version_Unhollower);
}