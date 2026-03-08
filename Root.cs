// SPDX-FileCopyrightText: 2023-2026 Neptuwunium
//
// SPDX-License-Identifier: 0BSD

using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

/// <summary>The root object for a glTF asset.</summary>
public class Root : Property {
	/// <summary>Names of glTF extensions used in this asset.</summary>
	[JsonPropertyName("extensionsUsed")]
	public HashSet<string>? ExtensionsUsed { get; set; }

	/// <summary>Names of glTF extensions required to properly load this asset.</summary>
	[JsonPropertyName("extensionsRequired")]
	public HashSet<string>? ExtensionsRequired { get; set; }

	/// <summary>An array of accessors. An accessor is a typed view into a bufferView.</summary>
	[JsonPropertyName("accessors")]
	public List<Accessor>? Accessors { get; set; }

	/// <summary>An array of keyframe animations.</summary>
	[JsonPropertyName("animations")]
	public List<Animation>? Animations { get; set; }

	/// <summary>Metadata about the glTF asset.</summary>
	[JsonPropertyName("asset")]
	public Asset Asset { get; set; } = new();

	/// <summary>An array of buffers. A buffer points to binary geometry, animation, or skins.</summary>
	[JsonPropertyName("buffers")]
	public List<Buffer>? Buffers { get; set; }

	/// <summary>An array of bufferViews. A bufferView is a view into a buffer generally representing a subset of the buffer.</summary>
	[JsonPropertyName("bufferViews")]
	public List<BufferView>? BufferViews { get; set; }

	/// <summary>An array of cameras. A camera defines a projection matrix.</summary>
	[JsonPropertyName("cameras")]
	public List<Camera>? Cameras { get; set; }

	/// <summary>An array of images. An image defines data used to create a texture.</summary>
	[JsonPropertyName("images")]
	public List<Image>? Images { get; set; }

	/// <summary>An array of materials. A material defines the appearance of a primitive.</summary>
	[JsonPropertyName("materials")]
	public List<Material>? Materials { get; set; }

	/// <summary>An array of meshes. A mesh is a set of primitives to be rendered.</summary>
	[JsonPropertyName("meshes")]
	public List<Mesh>? Meshes { get; set; }

	/// <summary>An array of nodes.</summary>
	[JsonPropertyName("nodes")]
	public List<Node>? Nodes { get; set; }

	/// <summary>An array of samplers. A sampler contains properties for texture filtering and wrapping modes.</summary>
	[JsonPropertyName("samplers")]
	public List<Sampler>? Samplers { get; set; }

	/// <summary>The index of the default scene. This property <b>MUST NOT</b> be defined, when `scenes` is undefined.</summary>
	[JsonPropertyName("scene")]
	public int Scene { get; set; }

	/// <summary>An array of scenes.</summary>
	[JsonPropertyName("scenes")]
	public List<Scene> Scenes { get; set; } = [
		new() {
			Name = "Scene",
		},
	];

	/// <summary>An array of skins. A skin is defined by joints and matrices.</summary>
	[JsonPropertyName("skins")]
	public List<Skin>? Skins { get; set; }

	/// <summary>An array of textures.</summary>
	[JsonPropertyName("textures")]
	public List<Texture>? Textures { get; set; }

	public static JsonSerializerOptions GltfJsonOptions =>
		new() {
			WriteIndented = true,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			IgnoreReadOnlyFields = true,
			IgnoreReadOnlyProperties = true,
			NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		};

	public static JsonSerializerOptions GLBJsonOptions =>
		new() {
			WriteIndented = false,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			IgnoreReadOnlyFields = true,
			IgnoreReadOnlyProperties = true,
			NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		};

	public void AddExtension<T>() where T : IExtension {
		ExtensionsUsed ??= [];
		ExtensionsUsed.Add(T.ExtensionName);
	}

	public void AddRequiredExtension<T>() where T : IExtension {
		ExtensionsRequired ??= [];
		ExtensionsRequired.Add(T.ExtensionName);
		AddExtension<T>();
	}

	public (Mesh Mesh, int Id) CreateMesh(string name) {
		Meshes ??= [];
		var id = Meshes.Count;
		var mesh = new Mesh {
			Name = name,
		};
		Meshes.Add(mesh);
		return (mesh, id);
	}

	public (Accessor Accessor, int Id) CreateAccessor<T>(
		Span<T> array,
		Stream buffer,
		BufferViewTarget? target,
		AccessorType type,
		AccessorComponentType componentType,
		int? stride = null,
		int? count = null,
		bool? normalized = null) where T : struct => CreateAccessor(CreateBufferView(MemoryMarshal.AsBytes(array), buffer, stride ?? Unsafe.SizeOf<T>(), target).Id, count ?? array.Length, 0, type, componentType, normalized);

	public (Accessor Accessor, int Id) CreateAccessor<T>(
		T[][] array,
		int size,
		Stream buffer,
		BufferViewTarget? target,
		AccessorType type,
		AccessorComponentType componentType,
		int? stride = null,
		int? count = null,
		bool? normalized = null) where T : struct {
		var tmp = new Span<T>(new T[size * array.Length]);
		for (var i = 0; i < array.Length; ++i) {
			array[i].AsSpan().CopyTo(tmp[(i * size)..]);
		}

		stride ??= Unsafe.SizeOf<T>();

		if (stride == -1) {
			stride = null;
		}

		return CreateAccessor(CreateBufferView(MemoryMarshal.AsBytes(tmp), buffer, stride, target).Id, count ?? array.Length, 0, type, componentType, normalized);
	}

	public (Accessor Accessor, int Id) CreateAccessor(int bufferView, int count, int offset, AccessorType type, AccessorComponentType componentType, bool? normalized) {
		Accessors ??= [];
		var id = Accessors.Count;
		var accessor = new Accessor {
			Name = null!,
			BufferView = bufferView,
			ByteOffset = offset,
			Count = count,
			Type = type,
			ComponentType = componentType,
			Normalized = normalized,
		};
		Accessors.Add(accessor);
		return (accessor, id);
	}

	public (BufferView View, int Id) CreateBufferView(ReadOnlySpan<byte> data, Stream buffer, int? stride, BufferViewTarget? target) {
		BufferViews ??= [];
		var id = BufferViews.Count;

		var offset = (int) buffer.Length;

		// align to 16.
		if (offset % 16 > 0) {
			var delta = 16 - offset % 16;
			buffer.Write(new byte[delta]);
			offset += delta;
		}

		var bufferView = new BufferView {
			Name = null!,
			ByteLength = data.Length,
			ByteOffset = offset,
			Buffer = 0,
			ByteStride = stride is null or <= 0 ? null : stride,
			Target = target,
		};
		BufferViews.Add(bufferView);
		buffer.Write(data);
		return (bufferView, id);
	}

	public (Texture Texture, int Id) CreateTexture(string name, string path, WrapMode wrapX, WrapMode wrapY, MagnificationFilter? mag, MinificationFilter? min) {
		Textures ??= [];
		var id = Textures.Count;
		var texture = new Texture {
			Name = name,
			Source = CreateImage(path).Id,
			Sampler = CreateSampler(mag, min, wrapX, wrapY).Id,
		};
		Textures.Add(texture);
		return (texture, id);
	}

	public (Sampler Sampler, int Id) CreateSampler(MagnificationFilter? mag, MinificationFilter? min, WrapMode wrapU, WrapMode wrapV) {
		Samplers ??= [];
		var sampler = new Sampler {
			Name = null!,
			MinificationFilter = min,
			MagnificationFilter = mag,
			WrapS = wrapU,
			WrapT = wrapV,
		};
		var id = Samplers.IndexOf(sampler);
		if (id > -1) {
			return (Samplers[id], id);
		}

		id = Samplers.Count;
		Samplers.Add(sampler);
		return (sampler, id);
	}

	private (Image Source, int Id) CreateImage(string path) {
		Images ??= [];
		var id = Images.Count;
		var image = new Image {
			Name = Path.GetFileNameWithoutExtension(path),
			Uri = path,
		};
		Images.Add(image);
		return (image, id);
	}

	public (Material Material, int Id) CreateMaterial(string name) {
		Materials ??= [];
		var id = Materials.Count;
		var material = new Material {
			Name = name,
		};
		Materials.Add(material);
		return (material, id);
	}

	public (Skin Skin, int Id) CreateSkin(string name) {
		Skins ??= [];
		var id = Skins.Count;
		var skin = new Skin {
			Name = name,
		};
		Skins.Add(skin);
		return (skin, id);
	}

	public (Node Node, int Id) CreateNode(string name, int? sceneId = null) {
		sceneId ??= Scene;
		var scene = Scenes[sceneId.Value];
		return scene.CreateNode(this, name);
	}

	public (Animation Animation, int Id) CreateAnimation(string name) {
		Animations ??= [];
		var id = Animations.Count;
		var animation = new Animation {
			Name = name,
		};
		Animations.Add(animation);
		return (animation, id);
	}

	public static Root FromGLB(Stream buffer, out int glbStart) {
		Span<int> header = stackalloc int[3];
		Span<int> atom = stackalloc int[2];
		glbStart = -1;

		buffer.ReadExactly(MemoryMarshal.AsBytes(header));

		if (header is not [0x46546C67, 2, _]) {
			throw new InvalidDataException();
		}

		buffer.ReadExactly(MemoryMarshal.AsBytes(atom));
		if (atom[0] != 0x4E4F534A) {
			throw new InvalidDataException();
		}

		var rented = ArrayPool<byte>.Shared.Rent(atom[1]);
		var rentSpan = rented.AsSpan(0, atom[1]);

		try {
			buffer.ReadExactly(rentSpan);
		} catch {
			ArrayPool<byte>.Shared.Return(rented);
			throw;
		}

		var gltf = JsonSerializer.Deserialize<Root>(Encoding.UTF8.GetString(rentSpan), GLBJsonOptions) ?? throw new InvalidDataException();

		if (header[2] - buffer.Position < 8) {
			return gltf;
		}

		buffer.ReadExactly(MemoryMarshal.AsBytes(atom));
	#pragma warning disable CA1508
		if (atom[0] != 0x4E4942) {
			throw new InvalidDataException();
		}

		if (atom[1] >= 4) {
			glbStart = checked((int) buffer.Position);
		}
	#pragma warning restore CA1508

		return gltf;
	}

	public Stream MakeGLB(Stream buffer) {
		var memory = new MemoryStream();
		MakeGLB(buffer, memory);
		return memory;
	}

	public void MakeGLB(Stream buffer, Stream target) {
		Span<int> header = stackalloc int[3];
		Span<int> atom = stackalloc int[2];
		Span<byte> align = stackalloc byte[4];

		Buffers ??= [];
		if (Buffers.Count == 0) {
			Buffers.Add(new Buffer {
				ByteLength = buffer.Length,
				Name = null,
			});
		}

		header[0] = 0x46546C67; // glTF
		header[1] = 2;
		header[2] = 0;
		target.Write(MemoryMarshal.AsBytes(header));

		var jsonText = JsonSerializer.Serialize(this, GLBJsonOptions);
		var jsonLength = Encoding.UTF8.GetByteCount(jsonText);
		if ((jsonLength & 3) != 0) {
			var old = jsonLength;
			jsonLength = (jsonLength + 3) & 0x7FFFFFFC;
			jsonText += new string(' ', jsonLength - old);
		}

		atom[0] = jsonLength;
		atom[1] = 0x4E4F534A; // JSON
		target.Write(MemoryMarshal.AsBytes(atom));
		target.Write(Encoding.UTF8.GetBytes(jsonText));

		var rawBufferLength = checked((int) buffer.Length);
		var bufferLength = unchecked(rawBufferLength + (4 - 1)) & ~(4 - 1);
		var extra = bufferLength - rawBufferLength;

		atom[0] = bufferLength;
		atom[1] = 0x4E4942; // BIN
		target.Write(MemoryMarshal.AsBytes(atom));
		buffer.Position = 0;
		buffer.CopyTo(target);

		if (extra > 0) {
			target.Write(align[..extra]);
		}

		target.Position = 0;
		header[2] = (int) target.Length;
		target.Write(MemoryMarshal.AsBytes(header));
	}
}
