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
      },{
        path:'employee',
        name:'employee',
        component:()=>import('../views/Administrative/Employee/Employee.vue')
      }
    ]
  }, {
    path: '/video',
    component: () => import('../views/Common/Video.vue')
  }, {
    path: '/Chat',
    component: () => import('../views/Chat/ChatPopup.vue')
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router