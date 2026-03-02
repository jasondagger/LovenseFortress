
using LovenseFortress.Core.Tools.ResourcePaths;

namespace LovenseFortress.Core.Services.StreamStates;

using LovenseFortress.Core.Tools.ResourcePaths;

internal sealed class ServiceStreamStateDefault() :
	ServiceStreamState(),
	IServiceStreamState
{
	void IServiceStreamState.Load()
	{
		base.AddStreamStateSceneToStreamStatesScene(
			filePathStreamState: _ = ResourcePaths.StreamStateDefault
		);
	}

	void IServiceStreamState.Unload()
	{
		base.RemoveStreamStateSceneFromMainScene();
	}
}