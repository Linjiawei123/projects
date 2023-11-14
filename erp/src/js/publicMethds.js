export function findParents(data, id, parents = []) {
  for (var i = 0; i < data.length; i++) {
    var node = data[i];
    if (node.id === id) {
      parents.push(node);
      return parents;
    } else if (node.child) {
      var result = findParents(node.child, id, parents.concat(node));
      if (result) {
        return result;
      }
    }
  }
  return [];
};

export function getParents(data, id) {
  var parents = findParents(data, id);
  // 去除同级节点
  if (parents.length > 1) {
    parents.pop();
  }
  return parents;
};

export function jsonTreeToArray(jsonTree) {
  var result = [];

  function flatten(node, depth) {
    result.push({
      ...node,
      depth
    });

    if (node.child) {
      node.child.forEach(child => {
        flatten(child, depth + 1);
      });
    }
  }

  jsonTree.forEach(node => {
    flatten(node, 0);
  });
  return result;
};

export function parentTree(child, data) {
  var parents = {};
  if (data != null && data.length > 0) {
    var parent = data.find(w => w.id === child.parentId);
    if (parent != null && parent != 'undefine') {
      parent.child = [];
      parent.child.push(child);
      parents = parent;
      if (parent.parentId != '00000000-0000-0000-0000-000000000000')
        parents = parentTree(parent, data);
    }
  }
  return parents;
};

export function getParentIds(child, data,parentIds = []) {
  if (data != null && data.length > 0) {
    var parent = data.find(w => w.id === child.parentId);
    if (parent != null && parent != 'undefine') {
      parentIds.unshift(parent.id);
      if (parent.parentId != '00000000-0000-0000-0000-000000000000'){
        parentIds = getParentIds(parent, data,parentIds);
      } 
    }
  }
  return parentIds;
};


export function getChildrenIds(node, ids = []) {
  if(node.id){
    ids.push(node.id);
  }
  if (node.children) {
    node.children.forEach(child => {
      getChildrenIds(child, ids);
    });
  }
  return ids;
};
