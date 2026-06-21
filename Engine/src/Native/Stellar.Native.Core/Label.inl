#pragma once

constexpr Label::Label(std::string_view name)
    : _name(name), _uid(Identifier::Null())
{
}


constexpr Label::Label(std::string_view name, Identifier id)
    : _name(name), _uid(id)
{
}
