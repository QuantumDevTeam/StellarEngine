#pragma once

// pragma

#define STELLAR_PRAGMA(x) _Pragma(#x)

#define STELLAR_CLANG_IGNORE_START()\
STELLAR_PRAGMA(clang diagnostic push)

#define STELLAR_CLANG_IGNORE_ADD(which)\
STELLAR_PRAGMA(clang diagnostic ignored which)

#define STELLAR_CLANG_IGNORE_END()\
STELLAR_PRAGMA(clang diagnostic pop)

#define STELLAR_CLANG_IGNORE(which)\
STELLAR_CLANG_IGNORE_START() \
STELLAR_CLANG_IGNORE_ADD(which)

// class and struct members

#define STELLAR_CONSTRUCT(Type)\
Type() = default

#define STELLAR_DECONSTRUCT(Type, modifyer)\
~Type() modifyer = default

#define STELLAR_CONSTRUCTION(Type, modifyer, secondModifyer)\
STELLAR_CONSTRUCT(Type);\
modifyer STELLAR_DECONSTRUCT(Type, secondModifyer)

#define STELLAR_DEFAULT_COPY_OPERATORS(Type)\
Type(const Type&) = default;\
Type(Type&&) noexcept = default;\
Type& operator=(const Type&) = default;\
Type& operator=(Type&&) noexcept = default

#define STELLAR_DELETE_COPY_OPERATORS(Type)\
Type(const Type&) = delete;\
Type& operator=(const Type&) = delete;\
Type(Type&&) = delete;\
Type& operator=(Type&&) = delete

#define STELLAR_CLASS_NAME_DEF(Type, modifyer, secondModifyer, thirdModifyer)\
    modifyer static const char* StaticClassName() { return "Native."#Type ; }\
    modifyer secondModifyer const char* GetClassName() const thirdModifyer { return StaticClassName(); }

#define STELLAR_TO_STRING()\
std::string ToString() const noexcept

#define STELLAR_HASHCODE()\
uint64_t GetHashCode() const noexcept

#define STELLAR_SPACESHIP(Type)\
auto operator<=>(const Type&) const noexcept = default

#define STELLAR_DEFAULTS(Type, modifyer, secondModifyer)\
[[nodiscard]] modifyer STELLAR_TO_STRING() secondModifyer;\
[[nodiscard]] modifyer STELLAR_HASHCODE() secondModifyer;\
[[nodiscard]] STELLAR_SPACESHIP(Type)

#define STELLAR_INLINE_DEFAULTS(Type, modifyer, secondModifyer)\
[[nodiscard]] modifyer STELLAR_TO_STRING() secondModifyer { return std::string(std::string(StaticClassName()) + "#" + to_string(GetUID())); }\
[[nodiscard]] modifyer STELLAR_HASHCODE() secondModifyer { return GetUID().GetHashCode(); }\
[[nodiscard]] STELLAR_SPACESHIP(Type);

#define STELLAR_INLINE_UID(id)\
[[nodiscard]] const Stellar::Native::Core::Identifier& GetUID() const override { return id; }

#define STELLAR_INLINE_UID_SIMPLE(id)\
[[nodiscard]] const Stellar::Native::Core::Identifier& GetUID() const { return id; }

#define STELLAR_GENERATE_BODY(Type, modifyer, secondModifyer, thirdModiyer)\
    modifyer STELLAR_CONSTRUCTION(Type, secondModifyer, thirdModiyer);\
public:\
    STELLAR_DEFAULT_COPY_OPERATORS(Type);\
    STELLAR_CLASS_NAME_DEF(Type, modifyer, secondModifyer, thirdModiyer)

#define STELLAR_GENERATE_INTERFACE(Type)\
public: STELLAR_GENERATE_BODY(Type, constexpr, virtual)

#define STELLAR_GENERATE_SINGLETON(Type, modifyer, secondModifyer)\
private:\
    STELLAR_GENERATE_BODY(Type, modifyer, secondModifyer)\
public:\
    modifyer static Type& GetInstance() {\
        static Type instance;\
        return instance;\
    }

#define STELLAR_GENERATE_QUANT(Type)\
STELLAR_GENERATE_BODY(Type, constexpr, , noexcept override)\
STELLAR_INLINE_DEFAULTS(Type, constexpr, override)

#define STELLAR_GENERATE_TO_STRING(Type)\
[[nodiscard]] inline std::string to_string(const Type& obj) noexcept { return obj.ToString(); }

#define STELLAR_GENERATE_HASHER(FullType)\
template <>\
struct std::hash<FullType>\
{\
    size_t operator()(const FullType& obj) const noexcept\
    {\
        return obj.GetHashCode();\
    }\
};

#define STELLAR_GENERATE_DEFAULTS(Namespace, Type)\
namespace Namespace\
{\
    STELLAR_GENERATE_TO_STRING(Type)\
}\
\
STELLAR_GENERATE_HASHER(Namespace::Type)
