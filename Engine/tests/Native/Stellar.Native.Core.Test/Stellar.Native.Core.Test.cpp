#include <cstdio>

// TODO: import module

int main(int argc, char* argv[])
{
    auto lbl = Stellar::Native::Core::Label("test");
    
    printf("{}", lbl.IsBound());
    
    return 0;
}
