import { createRouter, createWebHashHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'login',
    component: () => import('../views/LoginView.vue')
  }, {
    path: '/home',
    name: 'home',
    redirect: "/home/main",
    component: () => import('../views/HomeView.vue'),
    children: [
      {
        path: 'main',
        name: 'main',
        component: () => import('../views/MainView.vue')
      },
      {
        path: 'menu',
        name: 'menu',
        component: () => import('../views/System/Menu/Menu.vue')
      }, {
        path: 'user',
        name: 'user',
        component: () => import('../views/System/User/User.vue')
      }, {
        path: 'role',
        name: 'role',
        component: () => import('../views/System/Role/Role.vue')
      }, {
        path: 'department',
        name: 'department',
        component: () => import('../views/Administrative/Department/Department.vue')
      }, {
        path: 'loginLog',
        name: 'loginLog',
        component: () => import('../views/System/Logs/LoginLog.vue')
      }, {
        path: 'operateLog',
        name: 'operateLog',
        component: () => import('../views/System/Logs/OperateLog.vue')
      }, {
        path: 'employee',
        name: 'employee',
        component: () => import('../views/Administrative/Employee/Employee.vue')
      }, {
        path: 'inventoryType',
        name: 'inventoryType',
        component: () => import('../views/Base/Inventory/InventoryType.vue')
      }, {
        path: 'depType',
        name: 'depType',
        component: () => import('../views/Base/Dep/DepType.vue')
      }, {
        path: 'costType',
        name: 'costType',
        component: () => import('../views/Base/Cost/CostType.vue')
      }, {
        path: 'inven',
        name: 'inven',
        component: () => import('../views/Base/Inventory/Inven.vue')
      }, {
        path: 'supplier',
        name: 'supplier',
        component: () => import('../views/Base/Supplier.vue')
      }, {
        path: 'customer',
        name: 'customer',
        component: () => import('../views/Base/Customer.vue')
      }, {
        path: 'bsEmployee',
        name: 'bsEmployee',
        component: () => import('../views/Base/Dep/BSEmployee.vue')
      }, {
        path: 'cost',
        name: 'cost',
        component: () => import('../views/Base/Cost/Cost.vue')
      }, {
        path: 'store',
        name: 'store',
        component: () => import('../views/Base/Store.vue')
      }, {
        path: 'account',
        name: 'account',
        component: () => import('../views/Base/BSAccount.vue')
      }
    ]
  }, {
    path: '/video',
    component: () => import('../views/Common/Video.vue')
  }, {
    path: '/table',
    component: () => import('../components/CommonTable.vue')
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
