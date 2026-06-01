#pragma once

#define STELLAR_DEFINE_CS_METHODS()\
[[nodiscard]] std::string ToString() const noexcept;\
[[nodiscard]] uint64_t GetHashCode() const noexcept

#define STELLAR_DEFINE_TO_STRING(Type) \
[[nodiscard]] inline std::string to_string(const Type& obj) noexcept { return obj.ToString(); }

#define STELLAR_DEFINE_HASHER(Type)\
template <>\
struct std::hash<Type>\
{\
size_t operator()(const Type& obj) const noexcept\
{\
return obj.GetHashCode();\
}\
}

#define STELLAR_GENERATE_DEFAULT_ALLOCATOR(Type)\
Type() = default\

#define STELLAR_GENERATE_DEFAULT_DEALLOCATOR(Type)\
~Type() = default\

#define STELLAR_GENERATE_DEFAULT_ALLOCATION(Type)\
STELLAR_GENERATE_DEFAULT_ALLOCATOR(Type);\
STELLAR_GENERATE_DEFAULT_DEALLOCATOR(Type)

#define STELLAR_GENERATE_DEFAULT_VIRTUAL_ALLOCATION(Type)\
STELLAR_GENERATE_DEFAULT_ALLOCATOR(Type);\
virtual STELLAR_GENERATE_DEFAULT_DEALLOCATOR(Type)

#define STELLAR_PREPARE_INTERFACE(Type)\
Type(const Type&) = default;\
Type(Type&&) noexcept = default;\
Type& operator=(const Type&) = default;\
Type& operator=(Type&&) noexcept = default

#define STELLAR_PREPARE_INTERFACE_FULL(Type)\
STELLAR_GENERATE_DEFAULT_VIRTUAL_ALLOCATION(Type);\
STELLAR_PREPARE_INTERFACE(Type)
