using System.Text.Json.Serialization;

namespace GLTF.Scaffold;

/// <summary>The root nodes of a scene.</summary>
public record Scene : ChildOfRootProperty, INodeCreator {
	/// <summary>The indices of each root node.</summary>
	[JsonPropertyName("nodes")]
	public List<int> Nodes { get; set; } = [];

	public (Node Node, int Id) CreateNode(Root root) {
		var node = new Node();
		root.Nodes ??= [];
		var id = root.Nodes.Count;
		root.Nodes.Add(node);
		Nodes.Add(id);
		node.Id = id;
		return (node, id);
	}
}
