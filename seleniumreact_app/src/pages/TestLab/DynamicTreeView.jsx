import React, { useState,useCallback } from "react";
import { useStylesTree } from "./styleTree";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import ExpandLessIcon from '@mui/icons-material/ExpandLess';
import AddIcon from '@mui/icons-material/Add';
import Typography from '@mui/material/Typography';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import CancelIcon from '@mui/icons-material/Cancel';

const data = [
  {
    "name": "Root",
    "id": 1,
    "parentId": 0
  },
  {
    "name": "Root 2",
    "id": 10,
    "parentId": 0
  },
  {
    "name": "data 1",
    "id": 2,
    "parentId": 1
  },
  {
    "name": "data 2",
    "id": 3,
    "parentId": 1
  },
  {
    "name": "data 3",
    "id": 4,
    "parentId": 3
  }
];

const Card = ({newElementName, setNewElementName,
  toggleExpand,
  handleCRUDCancel ,
  handleKeyPress,
  handleDelete,
  setExpanded,expanded,
  handleCRUD, handleEdit, editMode, editData, setEditData, setEditMode, handleEditChange, handleKeyPressEdit,  setListData, data, expandedData = true, nodeData = 0, handleCRUDAtParent, nodeCount = 0, handleNodeCount, expandedInputId, setExpandedInputId, handleTask }) => {
  const styleClass = useStylesTree();
  console.log(data)
  return (
    <>
      <ul style={{ display: (expandedData ? 'block' : 'none') }} className={(nodeData == 0 ? styleClass.rootNodeFolder : styleClass.child)}>
        {data.map(item => {
          if (item.parentId === nodeData) {
            return (
              <li key={item.id} className={styleClass.cardListHolder}>
                <div className={styleClass.cardListHolderList}>
                  {data.some(child => child.parentId === item.id) && (
                    <>
                      {expanded ? <ExpandMoreIcon onClick={toggleExpand} /> : <ExpandLessIcon onClick={toggleExpand} />}
                    </>
                  )}
                  {editMode === item.id &&
                    <div className={styleClass.updateEdit}>
                      <input type="text" value={editData} className={styleClass.editTheFolder}
                        onChange={(e) => handleEditChange(item.id, e.currentTarget.value)}
                        onKeyPress={(event) => handleKeyPressEdit(event, item.id,nodeCount)}
                      />
                      <CancelIcon sx={{ color: '#f74d4d' }} onClick={() => handleEdit(item.id, item.name, 'cancel')} />
                    </div>


                  }
                  {editMode !== item.id &&
                    <span onClick={handleTask} style={{
                      cursor: 'pointer',
                      fontSize: '18px'
                    }}>
                      {item.name}
                    </span>
                  }
                  <div className={styleClass.crud} style={{

                  }}>
                    <EditIcon sx={{ color: '#654df7' }} onClick={() => handleEdit(item.id, item.name)} style={{ cursor: 'pointer', marginLeft: '10px' }} />
                    <DeleteIcon sx={{ color: '#f74d4d' }} onClick={() => handleDelete(item.id)} style={{ cursor: 'pointer' }} />
                    {nodeCount < 5 && (
                      <AddIcon sx={{ color: '#654df7' }} onClick={(event) => handleCRUD(event,item.id)} style={{
                        marginLeft: 'auto'
                      }} />
                    )}
                  </div>

                </div>
                {(expandedInputId === item.id && expandedInputId != null ) && 
                <div className={styleClass.updateEdit} style={{ display: (expandedInputId === item.id ? 'block' : 'block') }}>
                <input type="text"
                  className={styleClass.editTheFolder}
                  value={newElementName}
                  key={item.id}
                  onChange={(e) => setNewElementName(e.target.value)}
                  onKeyPress={(event) => handleKeyPress(event, item.id,nodeCount)} />
                <CancelIcon sx={{ color: '#f74d4d' }} onClick={() => handleCRUDCancel()} />
              </div>
                }
                
                {data.some(child => child.parentId === item.id) && (
                  <Card
                    data={data}
                    handleEdit={handleEdit}
                    keyData={item.id}
                    nodeData={item.id}
                    handleCRUDAtParent={handleCRUDAtParent}
                    expandedData={expanded}
                    getparentId={item.parentId}
                    nodeCount={nodeData + 1}
                    handleNodeCount={handleNodeCount}
                    expandedInputId={expandedInputId}
                    setExpandedInputId={setExpandedInputId}
                    handleTask={handleTask}
                    setListData={setListData}
                    handleKeyPressEdit={handleKeyPressEdit}
                    handleEditChange={handleEditChange}
                    editData={editData}
                    setEditData={setEditData}
                    editMode={editMode}
                    setEditMode={setEditMode}
                    newElementName={newElementName} 
                    setNewElementName={setNewElementName}                   
                    handleCRUD={handleCRUD}
                    expanded={expanded} 
                    setExpanded={setExpanded}
                    toggleExpand={toggleExpand}
                    handleCRUDCancel ={handleCRUDCancel}
                    handleKeyPress={handleKeyPress}
                    handleDelete={handleDelete} 
                  />
                )}
              </li>
            );
          }
          return null;
        })}
      </ul>
    </>
  );
};

const DynamicTreeView = ({ TestCaseHandle }) => {
  const [listData, setListData] = useState(data);
  const [nodeCount, setNodeCount] = useState(0);
  const [expandedInputId, setExpandedInputId] = useState(null);
  const [editData, setEditData] = useState(''); // State to store the value of the input field
  const [editMode, setEditMode] = useState(0); // State to store the value of the input field
  const [expanded, setExpanded] = useState(false);

  const [newElementName, setNewElementName] = useState(''); // State to store the value of the input field
  const handleCRUD = (event,parentId) => {
    event.preventDefault();
    console.log(parentId);
    // For demonstration purposes, let's add a new element if the node count is less than 4
    if (nodeCount < 5) {
      setExpandedInputId(parentId);
    } else {
      alert('Maximum node limit reached.');
    }
  };
  const handleCRUDAtParent = (newItem) => {
    setListData([...listData, newItem]);
  };

  const handleCRUDNewItem = useCallback((parentId,nodeData) => {
    
   
    if (nodeCount < 5) {
      setExpandedInputId(null); // Hide the input field
      if (newElementName) {
        const newId = Math.max(...listData.map(item => item.id)) + 1;
        const newItem = { name: newElementName, id: newId, parentId: parentId,new:'new' };
        handleCRUDAtParent(newItem);
        setExpanded(true);
        setNewElementName('');
      }
    } else {
      alert('Maximum node limit reached.');
    }
    console.log(listData);
  }, [nodeCount, newElementName, listData, handleCRUDAtParent]);
  


  const handleNodeCount = (count) => {
    
    setNodeCount(count);
  };
  const handleKeyPressEdit = (event, itemId,node) => {

  
    if (event.key === 'Enter') {
      setEditMode(0);
      const itemToEdit = listData.find(item => item.id === itemId);
      const newData = listData.filter((item) => {
        if (item.id !== itemId) {
          return item;
        } else if (item.id === itemId) {
          item.name = editData;
          return item;
        }
      });
      console.log()
      setListData(newData);
      setEditData('');
    }
  };
  const handleEdit = (itemId, name, mode = 'edit') => {
    if (mode == 'edit') {
      setEditMode( itemId);
      setEditData(name);
    } else if (mode == 'cancel') {
      setEditData('');
      setEditMode(0);
    }

  };

  const handleEditChange = (itemId, name) => {
    setEditData(name);
  };


  const toggleExpand = () => {
    setExpanded(!expanded);
  };
  const handleCRUDCancel = () => {
    setNewElementName('');
    setExpandedInputId(0);
  };


  const handleKeyPress = (event, parentId,nodeData) => {
    if (event.key === 'Enter') {
      handleCRUDNewItem(parentId,nodeData);
    }
  };
  const handleDelete = (itemId) => {
    console.log(itemId, data);
    const itemToDelete = data.find(item => item.id === itemId);
    const childrenToDelete = data.filter(item => item.parentId === itemId);
    const updatedData = data.filter(item => item.id !== itemId && item.parentId !== itemId);
    const parentIdOfParent = itemToDelete ? itemToDelete.parentId : null;
    const updatedChildren = childrenToDelete.map(child => ({
      ...child,
      parentId: parentIdOfParent,
    }));
    const newData = [...updatedData, ...updatedChildren];
    console.log(newData, itemId, itemToDelete, childrenToDelete);
    setListData(newData);
  };

  return (
    <div className="org-tree">
      <Card 
      handleEdit={handleEdit} 
      handleKeyPressEdit={handleKeyPressEdit} 
      handleEditChange={handleEditChange} 
      editData={editData} 
      setEditData={setEditData} 
      editMode={editMode} 
      setEditMode={setEditMode} 
      data={listData} 
      keyData={0} 
      handleTask={TestCaseHandle} 
      nodeData={0} 
      handleCRUDAtParent={handleCRUDAtParent} 
      nodeCount={nodeCount} 
      handleNodeCount={handleNodeCount} 
      expandedInputId={expandedInputId} 
      setExpandedInputId={setExpandedInputId} 
      setListData={setListData} 
      newElementName={newElementName} 
      setNewElementName={setNewElementName}
     
      handleCRUD={handleCRUD}
      expanded={expanded} 
      setExpanded={setExpanded}
      toggleExpand={toggleExpand}
      handleCRUDCancel ={handleCRUDCancel}
      handleKeyPress={handleKeyPress}
      handleDelete={handleDelete} 
      />
    </div>
  );
};

export default DynamicTreeView;