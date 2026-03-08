// SPDX-FileCopyrightText: 2023-2026 Neptuwunium
//
// SPDX-License-Identifier: 0BSD

namespace GLTF.Scaffold;

public interface INodeCreator {
	// ReSharper disable once UnusedMemberInSuper.Global
	public (Node Node, int Id) CreateNode(Root root, string name);
}
