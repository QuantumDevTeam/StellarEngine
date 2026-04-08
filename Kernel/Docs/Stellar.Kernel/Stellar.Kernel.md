# Stellar.Kernel assembly

## Stellar.Kernel namespace

| public type | description |
| --- | --- |
| interface [IIdentifier](./Stellar.Kernel/IIdentifier.md) | Base Engine unique Identifier |

## Stellar.Kernel.Configuration namespace

| public type | description |
| --- | --- |
| abstract class [ConfigurationComponent](./Stellar.Kernel.Configuration/ConfigurationComponent.md) | Runtime configuration Component data |
| enum [ConfigurationComponentBuildType](./Stellar.Kernel.Configuration/ConfigurationComponentBuildType.md) | Type of Runtime config components for S.R.I builder |
| abstract class [RuntimeConfiguration](./Stellar.Kernel.Configuration/RuntimeConfiguration.md) | Runtime config for EntryPoint |

## Stellar.Kernel.Configuration.Assets namespace

| public type | description |
| --- | --- |
| class [AssetData](./Stellar.Kernel.Configuration.Assets/AssetData.md) | Assets Data for Assets Config Component |
| class [AssetsComponent](./Stellar.Kernel.Configuration.Assets/AssetsComponent.md) | Assets Component for Runtime configuration |

## Stellar.Kernel.Data.Collections namespace

| public type | description |
| --- | --- |
| class [ConcurrentIdentifierMap&lt;T&gt;](./Stellar.Kernel.Data.Collections/ConcurrentIdentifierMap-1.md) | Map which use Identifier as key |
| interface [IDataContainer](./Stellar.Kernel.Data.Collections/IDataContainer.md) | Quant which can contain any other QuantumObject |

## Stellar.Kernel.Data.Context namespace

| public type | description |
| --- | --- |
| interface [IContext&lt;TData&gt;](./Stellar.Kernel.Data.Context/IContext-1.md) | Execution context |
| interface [IContextData](./Stellar.Kernel.Data.Context/IContextData.md) | Data of a Context |

## Stellar.Kernel.Data.Registry namespace

| public type | description |
| --- | --- |
| interface [IRegistry&lt;T&gt;](./Stellar.Kernel.Data.Registry/IRegistry-1.md) | Registry for QuantumObjects |

## Stellar.Kernel.EntryPoint namespace

| public type | description |
| --- | --- |
| interface [IModuleRunContextData](./Stellar.Kernel.EntryPoint/IModuleRunContextData.md) | Context of running any Module, of any Entrypoint |
| interface [IStopContextData](./Stellar.Kernel.EntryPoint/IStopContextData.md) | Context of stopping execution |
| abstract class [StellarEntryPoint](./Stellar.Kernel.EntryPoint/StellarEntryPoint.md) | Base class for all EntryPoint's |
| class [StellarEntryPointAttribute](./Stellar.Kernel.EntryPoint/StellarEntryPointAttribute.md) | Marc a Type implements EntryPoint as main startable |
| enum [StopReason](./Stellar.Kernel.EntryPoint/StopReason.md) | Reason for stoping execution |

## Stellar.Kernel.Failures namespace

| public type | description |
| --- | --- |
| [Flags] enum [FailureType](./Stellar.Kernel.Failures/FailureType.md) | Type of Failures |
| interface [IFailure](./Stellar.Kernel.Failures/IFailure.md) | Failure caught in engine or game operations |
| interface [IFailureCatch](./Stellar.Kernel.Failures/IFailureCatch.md) | Failure Catcher |
| interface [IFailureContextData](./Stellar.Kernel.Failures/IFailureContextData.md) | Failure Context data |
| interface [IFailureDispatcher](./Stellar.Kernel.Failures/IFailureDispatcher.md) | Failure main dispatcher for handling exceptions |
| interface [IFailureLevel](./Stellar.Kernel.Failures/IFailureLevel.md) | Failure Level. Indicate Failure behavior |

## Stellar.Kernel.Failures.Handlers namespace

| public type | description |
| --- | --- |
| interface [IFailureHandler](./Stellar.Kernel.Failures.Handlers/IFailureHandler.md) | Handler of a Failure |
| interface [IFailureHandlerProvider](./Stellar.Kernel.Failures.Handlers/IFailureHandlerProvider.md) | Handler provider for Handler registered in this Provider |

## Stellar.Kernel.FileSystem namespace

| public type | description |
| --- | --- |
| enum [DomainType](./Stellar.Kernel.FileSystem/DomainType.md) | Domain Types |
| interface [IDomain](./Stellar.Kernel.FileSystem/IDomain.md) | An abstract Domain for file location |
| interface [IFile](./Stellar.Kernel.FileSystem/IFile.md) | An abstract Quantum File stored in abstract Quantum Location |
| interface [IFileInfo](./Stellar.Kernel.FileSystem/IFileInfo.md) | Provides metadata information about a file. |
| interface [IFileStream](./Stellar.Kernel.FileSystem/IFileStream.md) | Abstract Quantum File stream for operating with file content |
| interface [IFileType](./Stellar.Kernel.FileSystem/IFileType.md) | Quantum File type |
| interface [ILocation](./Stellar.Kernel.FileSystem/ILocation.md) | An abstract Quantum Location in his Domain |

## Stellar.Kernel.FileSystem.Provider namespace

| public type | description |
| --- | --- |
| interface [IFileProvider](./Stellar.Kernel.FileSystem.Provider/IFileProvider.md) | File provider for handling Quantum File's |
| interface [IFileProviderFactory](./Stellar.Kernel.FileSystem.Provider/IFileProviderFactory.md) | File Provider Factory for identifying specific Provider by its Domain |

## Stellar.Kernel.Label namespace

| public type | description |
| --- | --- |
| interface [ILabel](./Stellar.Kernel.Label/ILabel.md) | Label linked to an Identifier |
| interface [ILabeled](./Stellar.Kernel.Label/ILabeled.md) | QuantumObject which has Label linked to his UID |

## Stellar.Kernel.Logging namespace

| public type | description |
| --- | --- |
| interface [ILogger](./Stellar.Kernel.Logging/ILogger.md) | Engine logger |
| enum [LogLevel](./Stellar.Kernel.Logging/LogLevel.md) | Level of log |

## Stellar.Kernel.Quantization namespace

| public type | description |
| --- | --- |
| interface [IIdentifiableQuantumObject](./Stellar.Kernel.Quantization/IIdentifiableQuantumObject.md) | QuantumObject which has UID |
| interface [IMetaQuant](./Stellar.Kernel.Quantization/IMetaQuant.md) | Quant's Meta |
| interface [IQuant](./Stellar.Kernel.Quantization/IQuant.md) | Base engine Type - Quant |
| interface [IQuantumObject](./Stellar.Kernel.Quantization/IQuantumObject.md) | Base QuantumObject |
| interface [IRegistrableMetaQuant](./Stellar.Kernel.Quantization/IRegistrableMetaQuant.md) | MetaQuant which can be registered in a registry |
| interface [IRegistrableQuant](./Stellar.Kernel.Quantization/IRegistrableQuant.md) | Quant which can be registered in a registry |
| interface [IRegistrableQuantumObject](./Stellar.Kernel.Quantization/IRegistrableQuantumObject.md) | QuantumObject which can be registered in a registry |

<!-- DO NOT EDIT: generated by xmldocmd for Stellar.Kernel.dll -->
