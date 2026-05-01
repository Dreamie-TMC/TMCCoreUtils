using Microsoft.Extensions.DependencyInjection;
using TMC.Tools.CoreLib.Core.Models;

namespace TMC.Tools.CoreLib.Core.Interfaces;

/// <summary>
/// Defines the methods required for trainer implementations for the minish cap combined toolset. Trainers are
/// dynamically discovered and instantiated at runtime by the toolset. Trainers are expected to have 2 constructors,
/// a parameterless one and one that takes in the proper parameters for dependency injection. The combined toolset
/// will provide the following dependencies by default:
/// - IDrawHandler
/// - ClientSurfaceDrawQueueHandler
/// - EmuSurfaceDrawQueueHandler
/// - IApiContainerWrapper
/// - IMemoryAccessor
/// Trainers are expected to use TryAddSingleton to register their dependencies.
/// </summary>
public interface ITrainer
{
    /// <summary>
    /// Trainers should implement this method for any processing that should happen <i>before</i> the emulator runs
    /// the next frame. UI elements will be drawn after all trainers run their UpdateBefore methods.
    /// </summary>
    void UpdateBefore();

    /// <summary>
    /// Trainers should implement this method for any processing that should happen <i>after</i> the emulator runs
    /// the current frame. UI elements can be queued in this method, but will not be drawn until before the next frame.
    /// </summary>
    void UpdateAfter();

    /// <summary>
    /// This gets called on core reboot and tool initialization and should be used to initialize/clear any stateful
    /// information the trainer maintains.
    /// </summary>
    void Restart();

    /// <summary>
    /// This method should add an instance of the current trainer to the service collection using
    /// TryAddSingleton<ITrainer, ClassName>(). Additionally, this should use the same method to add any dependencies
    /// it has that aren't provided by the combined toolset.
    /// </summary>
    /// <param name="serviceCollection"></param>
    void TryAddDependenciesToCollection(IServiceCollection serviceCollection);

    /// <summary>
    /// Returns the display name shown on the trainer's checkbox in the combined toolset UI.
    /// </summary>
    string GetCheckboxName();

    /// <summary>
    /// An asynchronous function that checks the github repository releases for the current trainer and returns a
    /// response containing information about the trainer update if it exists. This function is called once by the
    /// combined toolset on tool initialization.
    /// </summary>
    /// <returns></returns>
    Task<CheckForUpdateResponse> CheckForUpdates();
}
