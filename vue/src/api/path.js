import config from '../../public/app.config.json'
const base = {
    baseUrl: config.baseUrl,
    chatUrl: config.chatUrl,

    UploadPic: 'Upload/Upload',

    login: 'OA/SysLogin',
    loginUser: 'OA/LoginUser',
    menu: 'OA/SysMenu',
    menuRights: 'OA/SysMenu/MenuRights',
    addmenu: 'OA/SysMenu/Add',
    editmenu: 'OA/SysMenu/Update',
    delmenu: 'OA/SysMenu',
    permissionsList: 'OA/SysPermission/',
    permissionsAdd: 'OA/SysPermission',
    permissionsEdit: 'OA/SysPermission',
    permissionsDel: 'OA/SysPermission',
    user: 'OA/SysUser',
    role: 'OA/SysRole',
    roleUserList: 'OA/SysRole/UserList',
    roleUser: 'OA/SysRole/RoleUser',
    department: 'OA/Department',
    logs: 'OA/SystemLog',
    employee: 'OA/Employee',

}

export default base;