import config from '../../public/app.config.json'
const base = {
    baseUrl: config.baseUrl,

    UploadPic: 'Upload/Upload',

    login: 'Host/SysLogin',
    loginUser: 'Host/LoginUser',
    menu: 'Host/SysMenu',
    menuRights: 'Host/SysMenu/MenuRights',
    addmenu: 'Host/SysMenu/Add',
    editmenu: 'Host/SysMenu/Update',
    delmenu: 'Host/SysMenu',
    permissionsList: 'Host/SysPermission/',
    permissionsAdd: 'Host/SysPermission',
    permissionsEdit: 'Host/SysPermission',
    permissionsDel: 'Host/SysPermission',
    user: 'Host/SysUser',
    role: 'Host/SysRole',
    roleUserList: 'Host/SysRole/UserList',
    roleUser: 'Host/SysRole/RoleUser',
    department: 'Host/Department',
    logs: 'Host/SystemLog',
    employee: 'Host/Employee',

    inventoryType: 'Base/BSInvenType',
    DepType: 'Base/BSDepartment',
    CostType: 'Base/BSCostType',
    inven: 'Base/BSInven',
    supplier: 'Base/BSSupplier',
    customer: 'Base/BSCustomer',
    bsemployee: 'Base/BSEmployee',
    cost: 'Base/BSCost',
    store: 'Base/BSStore',
    account:'Base/BSAccount'

}

export default base;