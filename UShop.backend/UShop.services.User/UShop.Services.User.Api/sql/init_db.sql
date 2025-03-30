/**
  初始化数据库
  @by cps
  @date 2025-03-29
  @version 1.0.0
 */

-- 登录账号
drop table if exists account;
create table account(
  id bigint primary key,
  is_deleted boolean not null default false,
  name varchar(20) not null,
  password varchar(32) not null,
  phone varchar(11),
  avatar varchar(100) not null,
  realName varchar(50) not null,
  disabled boolean not null default false,
  disabled_time timestamp,
  disabled_reason varchar(100),
  created_by bigint not null,
  created_at timestamp default now(),
  last_updated_by bigint not null,
  last_updated_at timestamp default now()
);
comment on column account.id is '主键id';
comment on column account.is_deleted is '是否删除';
comment on column account.name is '登录账号';
comment on column account.password is '登录密码';
comment on column account.phone is '手机号码';
comment on column account.avatar is '头像';
comment on column account.realName is '真实姓名';
comment on column account.disabled is '是否禁用';
comment on column account.disabled_time is '禁用时间';
comment on column account.disabled_reason is '禁用原因';
comment on column account.created_by is '创建人';
comment on column account.created_at is '创建时间';
comment on column account.last_updated_by is '最后修改人';
comment on column account.last_updated_at is '最后修改时间';
-- 插入用户管理账号数据
INSERT INTO account (id, name, password, phone, avatar, realName, created_by, last_updated_by)
VALUES
(173566080000000001, 'admin', 'password123', '13800000001', 'http://example.com/avatar1.png', '管理员', 173566080000000001, 173566080000000001),
(173566080000000002, 'user', 'password456', '13800000002', 'http://example.com/avatar2.png', '普通用户', 173566080000000002, 173566080000000002),
(173566080000000003, 'testuser', 'password789', '13800000003', 'http://example.com/avatar3.png', '测试用户', 173566080000000003, 173566080000000003),
(173566080000000004, 'visitoruser1', 'guestpass', '13800000004', 'http://example.com/avatar4.png', '访客1', 173566080000000004, 173566080000000004),
(173566080000000005, 'Visitoruser2', 'testpass', '13800000005', 'http://example.com/avatar5.png', '访客2', 173566080000000005, 173566080000000005);


-- 角色
drop table if exists role;
create table role(
  id bigint primary key,
  is_deleted boolean not null default false,
  name varchar(20) not null,
  description varchar(100),
  disabled boolean not null default false,
  disabled_time timestamp,
  disabled_reason varchar(100),
  created_by bigint not null,
  created_at timestamp default now(),
  last_updated_by bigint not null,
  last_updated_at timestamp default now()
);
comment on column role.id is '主键id';
comment on column role.name is '角色名称';
comment on column role.description is '角色描述';
comment on column role.disabled is '是否禁用';
comment on column role.disabled_time is '禁用时间';
comment on column role.disabled_reason is '禁用原因';
comment on column role.created_by is '创建人';
comment on column role.created_at is '创建时间';
comment on column role.last_updated_by is '最后修改人';
comment on column role.last_updated_at is '最后修改时间';
-- 插入角色数据
INSERT INTO role (id, name, description, created_by, last_updated_by)
VALUES
(173566080000000006, '管理员角色', '管理员角色，具有所有权限', 173566080000000001, 173566080000000001),
(173566080000000007, '普通用户角色', '普通用户角色', 173566080000000002, 173566080000000002),
(173566080000000008, '访客角色', '访客角色，权限受限', 173566080000000003, 173566080000000003),
(173566080000000009, '经理角色', '经理角色，管理权限', 173566080000000004, 173566080000000004),
(173566080000000010, '开发者角色', '开发者角色', 173566080000000005, 173566080000000005);


-- 账号角色
drop table if exists account_role;
create table account_role(
  account_id bigint not null,
  role_id bigint not null,
  created_by bigint not null,
  created_at timestamp default now(),
  last_updated_by bigint not null,
  last_updated_at timestamp default now(),
  primary key(account_id, role_id)
);
comment on column account_role.account_id is '账号id';
comment on column account_role.role_id is '角色id';
comment on column account_role.created_by is '创建人';
comment on column account_role.created_at is '创建时间';
comment on column account_role.last_updated_by is '最后修改人';
comment on column account_role.last_updated_at is '最后修改时间';
-- 插入账号与角色关联数据
INSERT INTO account_role (account_id, role_id, created_by, last_updated_by)
VALUES
(173566080000000001, 173566080000000006, 173566080000000001, 173566080000000001),
(173566080000000002, 173566080000000007, 173566080000000002, 173566080000000002),
(173566080000000003, 173566080000000008, 173566080000000003, 173566080000000003),
(173566080000000004, 173566080000000008, 173566080000000004, 173566080000000004),
(173566080000000005, 173566080000000007, 173566080000000005, 173566080000000005);


-- 权限
drop table if exists permission;
create table permission(
  id bigint primary key,
  is_deleted boolean not null default false,
  menu_id bigint not null,
  name varchar(20) not null,
  code varchar(20) not null,
  description varchar(100),
  disabled boolean not null default false,
  disabled_time timestamp,
  disabled_reason varchar(100),
  created_by bigint not null,
  created_at timestamp default now(),
  last_updated_by bigint not null,
  last_updated_at timestamp default now()
);
comment on column permission.id is '主键id';
comment on column permission.menu_id is '菜单id';
comment on column permission.name is '权限名称';
comment on column permission.code is '权限代码';
comment on column permission.description is '权限描述';
comment on column permission.disabled is '是否禁用';
comment on column permission.disabled_time is '禁用时间';
comment on column permission.disabled_reason is '禁用原因';
comment on column permission.created_by is '创建人';
comment on column permission.created_at is '创建时间';
comment on column permission.last_updated_by is '最后修改人';
comment on column permission.last_updated_at is '最后修改时间';
-- 插入权限数据
INSERT INTO permission (id, menu_id, name, code, description, created_by, last_updated_by)
VALUES
(173566080000000016, 173566080000000011, '查看仪表板', 'dashboard:view', '查看仪表板', 173566080000000001, 173566080000000001),
(173566080000000017, 173566080000000012, '查看用户列表', 'user:view', '查看用户列表', 173566080000000002, 173566080000000002),
(173566080000000018, 173566080000000013, '新增用户', 'user:add', '新增用户', 173566080000000003, 173566080000000003),
(173566080000000019, 173566080000000014, '编辑用户', 'user:edit', '编辑用户', 173566080000000004, 173566080000000004),
(173566080000000020, 173566080000000015, '修改系统设置', 'settings:change', '修改系统设置', 173566080000000005, 173566080000000005);


-- 角色权限
drop table if exists role_permission;
create table role_permission(
  role_id bigint not null,
  permission_id bigint not null,
  created_by bigint not null,
  created_at timestamp default now(),
  last_updated_by bigint not null,
  last_updated_at timestamp default now(),
  primary key(role_id, permission_id)
);
comment on column role_permission.role_id is '角色id';
comment on column role_permission.permission_id is '权限id';
comment on column role_permission.created_by is '创建人';
comment on column role_permission.created_at is '创建时间';
comment on column role_permission.last_updated_by is '最后修改人';
comment on column role_permission.last_updated_at is '最后修改时间';
-- 插入角色与权限关联数据
INSERT INTO role_permission (role_id, permission_id, created_by, last_updated_by)
VALUES
(173566080000000006, 173566080000000016, 173566080000000001, 173566080000000001),
(173566080000000006, 173566080000000017, 173566080000000001, 173566080000000001),
(173566080000000006, 173566080000000018, 173566080000000001, 173566080000000001),
(173566080000000007, 173566080000000017, 173566080000000002, 173566080000000002),
(173566080000000007, 173566080000000019, 173566080000000002, 173566080000000002),
(173566080000000008, 173566080000000017, 173566080000000003, 173566080000000003),
(173566080000000008, 173566080000000020, 173566080000000003, 173566080000000003),
(173566080000000009, 173566080000000016, 173566080000000004, 173566080000000004),
(173566080000000009, 173566080000000018, 173566080000000004, 173566080000000004),
(173566080000000010, 173566080000000019, 173566080000000005, 173566080000000005);


-- 菜单
drop table if exists menu;
create table menu(
  id bigint primary key,
  parent_id bigint,
  is_deleted boolean not null default false,
  title varchar(20) not null,
  name varchar(50) not null,
  path varchar(100) not null,
  component varchar(100) not null,
  icon varchar(20),
  link varchar(100),
  is_hide boolean not null default false,
  is_full boolean not null default false,
  is_affix boolean not null default false,
  is_keep_alive boolean not null default true,
  index int not null,
  disabled boolean not null default false,
  disabled_time timestamp,
  disabled_reason varchar(100),
  created_by bigint not null,
  created_at timestamp default now(),
  last_updated_by bigint not null,
  last_updated_at timestamp default now()
);
comment on column menu.id is '主键id';
comment on column menu.parent_id is '父级菜单id';
comment on column menu.is_deleted is '是否删除';
comment on column menu.title is '菜单标题';
comment on column menu.name is '菜单名称, 与前端script setup的name一致 用于页面保活';
comment on column menu.path is '菜单路径';
comment on column menu.component is '菜单组件';
comment on column menu.icon is '菜单图标';
comment on column menu.link is '外链菜单打开方式';
comment on column menu.is_hide is '是否隐藏';
comment on column menu.is_full is '菜单是否为全屏';
comment on column menu.is_affix is '菜单是否固定';
comment on column menu.is_keep_alive is '菜单是否缓存';
comment on column menu.index is '菜单排序';
comment on column menu.disabled is '是否禁用';
comment on column menu.disabled_time is '禁用时间';
comment on column menu.disabled_reason is '禁用原因';
comment on column menu.created_by is '创建人';
comment on column menu.created_at is '创建时间';
comment on column menu.last_updated_by is '最后修改人';
comment on column menu.last_updated_at is '最后修改时间';
-- 插入菜单数据
INSERT INTO menu (id, parent_id, title, name, path, component, icon, link, is_hide, is_full, is_affix, is_keep_alive, index, created_by, last_updated_by)
VALUES
(173566080000000011, NULL, '仪表板', 'Dashboard', '/dashboard', 'DashboardComponent', 'dashboard-icon', NULL, FALSE, TRUE, FALSE, TRUE, 1, 173566080000000001, 173566080000000001),
(173566080000000012, NULL, '用户管理', 'UserManagement', '/user-management', 'UserManagementComponent', 'user-icon', NULL, FALSE, FALSE, FALSE, TRUE, 2, 173566080000000002, 173566080000000002),
(173566080000000013, 173566080000000012, '新增用户', 'AddUser', '/user-management/add', 'AddUserComponent', 'add-user-icon', NULL, FALSE, FALSE, FALSE, TRUE, 1, 173566080000000003, 173566080000000003),
(173566080000000014, 173566080000000012, '编辑用户', 'EditUser', '/user-management/edit', 'EditUserComponent', 'edit-user-icon', NULL, FALSE, FALSE, FALSE, TRUE, 2, 173566080000000004, 173566080000000004),
(173566080000000015, NULL, '系统设置', 'Settings', '/settings', 'SettingsComponent', 'settings-icon', NULL, FALSE, FALSE, TRUE, TRUE, 3, 173566080000000005, 173566080000000005);


-- 角色菜单
drop table if exists role_menu;
create table role_menu(
  role_id bigint not null,
  menu_id bigint not null,
  created_by bigint not null,
  created_at timestamp default now(),
  last_updated_by bigint not null,
  last_updated_at timestamp default now(),
  primary key(role_id, menu_id)
);
comment on column role_menu.role_id is '角色id';
comment on column role_menu.menu_id is '菜单id';
comment on column role_menu.created_by is '创建人';
comment on column role_menu.created_at is '创建时间';
comment on column role_menu.last_updated_by is '最后修改人';
comment on column role_menu.last_updated_at is '最后修改时间';
-- 插入角色与菜单关联数据
INSERT INTO role_menu (role_id, menu_id, created_by, last_updated_by)
VALUES
(173566080000000006, 173566080000000011, 173566080000000001, 173566080000000001),  -- 管理员角色关联仪表板菜单
(173566080000000006, 173566080000000012, 173566080000000001, 173566080000000001),  -- 管理员角色关联用户管理菜单
(173566080000000007, 173566080000000012, 173566080000000002, 173566080000000002),  -- 普通用户角色关联用户管理菜单
(173566080000000008, 173566080000000012, 173566080000000003, 173566080000000003),  -- 访客角色关联用户管理菜单
(173566080000000009, 173566080000000015, 173566080000000004, 173566080000000004),  -- 经理角色关联系统设置菜单
(173566080000000010, 173566080000000015, 173566080000000005, 173566080000000005);  -- 开发者角色关联系统设置菜单

